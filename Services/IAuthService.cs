using JwtAuthWebApi.DTOs.Requests;
using JwtAuthWebApi.DTOs.Responses;

namespace JwtAuthWebApi.Services;

public interface IAuthService
{
    Task<bool> AuthenticateAsync(LoginRequest request);
    public LoginResponse GenerateJwt(LoginRequest request);
}