using System.ComponentModel.DataAnnotations.Schema;

namespace JwtAuthWebApi.Models;

public class User
{
    public int Id { get; set; }
    
    [Column(TypeName = "varchar(100)")]
    public string Username { get; set; } = null!;
    
    [Column(TypeName = "varchar(50)")]
    public string Password { get; set; } = null!;

    public UserDetail UserDetail { get; set; } = null!;
}