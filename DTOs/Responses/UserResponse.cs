namespace JwtAuthWebApi.DTOs.Responses;

public class UserResponse
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Fullname { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
}