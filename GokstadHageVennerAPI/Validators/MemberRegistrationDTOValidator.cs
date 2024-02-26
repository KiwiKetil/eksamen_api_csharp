using FluentValidation;
using FluentValidation.AspNetCore;
using GokstadHageVennerAPI.Models.DTOs;

namespace GokstadHageVennerAPI.Validators;

public class MemberRegistrationDTOValidator : AbstractValidator<MemberRegistrationDTO>
{
    public MemberRegistrationDTOValidator()
    {
        RuleFor(x => x.UserName)
           .NotEmpty().WithMessage("UserName can not be null")
           .MinimumLength(3).WithMessage("UserName must contain at least 3 characters")
           .MaximumLength(16).WithMessage("UserName limit exceeded (max 16 characters)");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName can not be null")
            .MaximumLength(16).WithMessage("FirstName limit exceeded (max 16 characters)");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("LastName can not be null")
            .MaximumLength(16).WithMessage("LastName limit exceeded (max 16 characters)");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email must be included")
            .EmailAddress().WithMessage("Email must be valid");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password must be included")
            .MinimumLength(8).WithMessage("Password must contain at least 8 characters")
            .MaximumLength(16).WithMessage("Password limit exceeded (max 16 characters)")
            .Matches(@"[0-9]+").WithMessage("Password must contain at least 1 number")
            .Matches(@"[A-Z]+").WithMessage("Password must contain at least 1 uppercase letter")
            .Matches(@"[a-z]+").WithMessage("Password must contain at least 1 lowercase letter")
            .Matches(@"[!?*#_-]+").WithMessage("Password must contain at least 1 special character ('! ? * # _ -')");
    }
}
