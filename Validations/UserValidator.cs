using FluentValidation;
using JwtAuthWebApi.DTOs.Requests;

namespace JwtAuthWebApi.Validations;

public class UserValidator : AbstractValidator<UserRequest>
{
    public UserValidator()
    {
        RuleFor(user => user.Username)
            .NotEmpty().WithMessage("The 'Username field is required")
            .MinimumLength(3).WithMessage("Username must be at less than 3 characters");

        RuleFor(user => user.Fullname)
            .NotEmpty().WithMessage("The Fullname field is required")
            .Length(3, 50).WithMessage("Username must be between 3 and 50 characters");

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("The Password field is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters");

        RuleFor(user => user.DateOfBirth).NotEmpty().WithMessage("Date of birth is required");
    }
}