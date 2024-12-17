using System.Text;
using JwtAuthWebApi.DTOs.Responses;
using JwtAuthWebApi.Exceptions;
using JwtAuthWebApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthWebApi.Extensions;

public static class JwtConfigExtension
{
    public static void AddJwtConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
                {
                    var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

                    if (jwtSettings is null)
                        throw new ApiException("JWT Settings are not setup yet in appseting.json",
                            StatusCodes.Status500InternalServerError);

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";

                            return context.Response.WriteAsJsonAsync(
                                context.Exception is SecurityTokenExpiredException
                                    ? ApiResponse<object>.Failure("Token has expired")
                                    : ApiResponse<object>.Failure("Invalid Token")
                            );
                        },

                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            return context.Response.WriteAsJsonAsync(
                                ApiResponse<object>.Failure("You are not authorized to access this resource")
                            );
                        }
                    };
                }
            );
    }
}