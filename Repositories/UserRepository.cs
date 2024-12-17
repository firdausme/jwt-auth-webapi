using JwtAuthWebApi.Data;
using JwtAuthWebApi.Extensions;
using JwtAuthWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthWebApi.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<List<User>> GetAllAsync()
    {
        return await context.Users.Include(u => u.UserDetail).ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await context.Users.Include(u => u.UserDetail).FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await context.Users.Include(u => u.UserDetail).FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task CreateAsync(User user)
    {
        await context.Users.AddAsync(user.EncryptPassword());
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        context.Users.Remove(user);
        await context.SaveChangesAsync();
    }
}