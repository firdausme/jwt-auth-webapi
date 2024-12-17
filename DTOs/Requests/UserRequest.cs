namespace JwtAuthWebApi.DTOs.Requests;

public class UserRequest
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Fullname { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
}