using FluentValidation;
using GokstadHageVennerAPI.Models.DTOs;

namespace GokstadHageVennerAPI.Validators;

public class EventDTOValidator : AbstractValidator<EventDTO>
{
    public EventDTOValidator()
    {
        RuleFor(x => x.EventType)
            .NotEmpty().WithMessage("EventType can not be null")
            .MinimumLength(3).WithMessage("EventType must contain at least 3 characters")
            .MaximumLength(30).WithMessage("EventType limit exceeded (max 30 characters)");

        RuleFor(x => x.EventName)
            .NotEmpty().WithMessage("EventName can not be null")
            .MinimumLength(3).WithMessage("EventName must contain at least 3 characters")
            .MaximumLength(30).WithMessage("EventName limit exceeded (max 30 characters)");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description can not be null")
            .MaximumLength(200).WithMessage("Description limit exceeded (max 200 characters)");

        RuleFor(x => x.EventDate)
            .NotEmpty().WithMessage("Event date must be included");

        RuleFor(x => x.EventTime)
            .NotEmpty().WithMessage("Event time must be included");          
    }
}
