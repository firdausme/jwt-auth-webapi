using FluentValidation;
using JwtAuthWebApi.Mappers;
using JwtAuthWebApi.Repositories;
using JwtAuthWebApi.Services;
using JwtAuthWebApi.Validations;

namespace JwtAuthWebApi.Extensions;

public static class ServiceConfigExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();

        services.AddAutoMapper(typeof(UserMapper));

        // services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<UserValidator>();
        services.AddValidatorsFromAssemblyContaining<LoginValidator>();
    }
}