namespace JwtAuthWebApi.Models;

public class JwtSettings
{
    public string Key { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int ExpiresInMinute { get; set; } = -1;

    public override string ToString()
    {
        return $"Key:{Key}, Issuer:{Issuer}, Audience:{Audience}, ExpiresIn:{ExpiresInMinute}";
    }
}