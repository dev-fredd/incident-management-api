using FluentValidation;
using Incident.Application.Commands.CreateIncident;

namespace Incident.Application.Validators;

public class CreateIncidentValidator : AbstractValidator<CreateIncidentCommand>
{
    public CreateIncidentValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("El título es obligatorio.")
            .MaximumLength(100).WithMessage("El título no puede tener más de 100 caracteres.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("La descripción es obligatoria.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId es obligatorio.");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("CategoryId es obligatorio.");
    }
}
