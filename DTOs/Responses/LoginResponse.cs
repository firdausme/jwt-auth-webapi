namespace JwtAuthWebApi.DTOs.Responses;

public class LoginResponse
{
    public string Username { get; set; } = null!;
    public string Token { get; set; } = null!;
}