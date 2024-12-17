using JwtAuthWebApi.DTOs.Requests;
using JwtAuthWebApi.DTOs.Responses;
using JwtAuthWebApi.Exceptions;
using JwtAuthWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthWebApi.Controllers;

[ApiController, AllowAnonymous]
[Route("api/[controller]")]
public class AuthController(IAuthService service, ILogger<AuthController> log) : Controller
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var authenticate = await service.AuthenticateAsync(request);
        if (authenticate)
        {
            return Ok(
                ApiResponse<LoginResponse>.Success(service.GenerateJwt(request), "Login successful")
            );
        }

        log.LogInformation("Invalid login attempt.");
        throw new ApiException("Invalid username or password", StatusCodes.Status401Unauthorized);
    }
}