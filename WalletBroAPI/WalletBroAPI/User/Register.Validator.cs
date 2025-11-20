using FastEndpoints;
using FluentValidation;

namespace WalletBroAPI.User;

public class RegisterValidator : Validator<RegisterRequest>
{
    public RegisterValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName is required");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is invalid");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters");
            
        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required")
            .LessThan(DateTime.Today).WithMessage("Date of birth cannot be in the future")
            .Must(d => d <= DateTime.Today.AddYears(-18)).WithMessage("You must be at least 18 years old")
            .GreaterThan(DateTime.Today.AddYears(-120)).WithMessage("Date of birth is not valid");
    }
}