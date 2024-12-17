using JwtAuthWebApi.Models;

namespace JwtAuthWebApi.Extensions;

public static class UserPasswordExtension
{
    public static User EncryptPassword(this User user)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        return user;
    }

    public static bool VerifyPassword(this User user, string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, user.Password);
    }
}

