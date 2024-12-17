using JwtAuthWebApi.Extensions;
using JwtAuthWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthWebApi.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<UserDetail> UserDetails => Set<UserDetail>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(u => u.UserDetail)
            .WithOne(u => u.User)
            .HasForeignKey<UserDetail>(u => u.UserId);

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "adi", Password = "adi123!!" }.EncryptPassword(),
            new User { Id = 2, Username = "budi", Password = "budi!!" }.EncryptPassword(),
            new User { Id = 3, Username = "caca", Password = "caca!!" }.EncryptPassword()
        );

        modelBuilder.Entity<UserDetail>().HasData(
            new UserDetail { Id = 1, FullName = "Adi Wibowo", DateOfBirth = new DateTime(2000, 1, 1), UserId = 1, },
            new UserDetail { Id = 2, FullName = "Budi Darwan", DateOfBirth = new DateTime(2002, 2, 24), UserId = 2 },
            new UserDetail { Id = 3, FullName = "Caca Handika", DateOfBirth = new DateTime(1998, 4, 12), UserId = 3 }
        );
    }
}