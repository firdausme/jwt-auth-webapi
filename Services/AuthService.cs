using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FluentValidation;
using JwtAuthWebApi.DTOs.Requests;
using JwtAuthWebApi.DTOs.Responses;
using JwtAuthWebApi.Extensions;
using JwtAuthWebApi.Models;
using JwtAuthWebApi.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthWebApi.Services;

public class AuthService(
    IUserRepository repository,
    IValidator<LoginRequest> validator,
    IOptions<JwtSettings> jwSettings
) : IAuthService
{
    private readonly JwtSettings _settings = jwSettings.Value;

    public async Task<bool> AuthenticateAsync(LoginRequest request)
    {
        await validator.ValidateAndThrowAsync(request);

        var user = await repository.GetByUsernameAsync(request.Username);

        return user?.VerifyPassword(request.Password) ?? false;
    }

    public LoginResponse GenerateJwt(LoginRequest request)
    {
        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: new List<Claim>()
            {
                new("username", request.Username),
            },
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key)),
                SecurityAlgorithms.HmacSha256
            )
        );

        return new LoginResponse()
        {
            Username = request.Username,
            Token = new JwtSecurityTokenHandler().WriteToken(token),
        };
    }
}