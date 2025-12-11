using FluentValidation;
using Incident.Application.Commands.UpdateIncident;
using Incident.Domain.Enums;

namespace Incident.Application.Validators;

public class UpdateIncidentValidator : AbstractValidator<UpdateIncidentCommand>
{
    public UpdateIncidentValidator()
    {
        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("El estado del incidente no es válido.");
            
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("El Id del incidente es obligatorio.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("El título es obligatorio.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("La descripción es obligatoria.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("El estado del incidente no es válido.");
    }
}
