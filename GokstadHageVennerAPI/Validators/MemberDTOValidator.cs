using FluentValidation;
using GokstadHageVennerAPI.Models.DTOs;

namespace GokstadHageVennerAPI.Validators;

public class MemberDTOValidator : AbstractValidator<MemberDTO>
{
    public MemberDTOValidator()
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
    }
}
