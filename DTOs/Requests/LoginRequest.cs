using System.ComponentModel.DataAnnotations;

namespace JwtAuthWebApi.DTOs.Requests;

public class LoginRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}