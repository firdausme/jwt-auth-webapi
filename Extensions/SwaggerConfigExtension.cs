using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace JwtAuthWebApi.Extensions;

public static class SwaggerConfigExtension
{
    public static void AddSwaggerGenExt(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: Bearer {token}",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            options.OperationFilter<AuthOperationsFilter>();
        });
    }

    private class AuthOperationsFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Check if the action or controller has [AllowAnonymous]
            if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor apiDescriptor)
            {
                var controllerType = apiDescriptor.ControllerTypeInfo;
                if (controllerType.IsDefined(typeof(AllowAnonymousAttribute), true)) return;

                if (apiDescriptor.MethodInfo.IsDefined(typeof(AllowAnonymousAttribute), true)) return;
            }

            // Add Api security requirements
            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                }
            };
        }
    }
}