using FluentValidation;
using JwtAuthWebApi.DTOs.Requests;

namespace JwtAuthWebApi.Validations;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(r => r.Username).NotEmpty().WithMessage("The Username field is required.");
        RuleFor(r => r.Password).NotEmpty().WithMessage("The Password field is required.");
    }
}