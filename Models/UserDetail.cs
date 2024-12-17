using System.ComponentModel.DataAnnotations.Schema;

namespace JwtAuthWebApi.Models;

public class UserDetail
{
    public int Id { get; set; }
    
    [Column(TypeName = "varchar(100)")]
    public string FullName { get; set; } = null!;
    
    [Column(TypeName = "date")]
    public DateTime DateOfBirth { get; set; }
    
    public int UserId { get; set; }
    
    public User User { get; set; } = null!;

}