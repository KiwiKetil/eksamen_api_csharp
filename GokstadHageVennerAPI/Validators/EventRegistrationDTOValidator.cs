using FluentValidation;
using GokstadHageVennerAPI.Models.DTOs;

namespace GokstadHageVennerAPI.Validators;

public class EventRegistrationDTOValidator : AbstractValidator<EventRegistrationDTO>
{
    public EventRegistrationDTOValidator()
    {
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status can not be null")          
            .MaximumLength(30).WithMessage("Status limit exceeded (max 30 characters)");
    }
}
