using FluentValidation;
using Incident.Application.Commands.AddComment;

namespace Incident.Application.Validators;


public class AddCommentValidator : AbstractValidator<AddCommentCommand>
{
    public AddCommentValidator()
    {
        RuleFor(x => x.IncidentId)
            .NotEmpty().WithMessage("El Id del incidente es obligatorio.");

        RuleFor(x => x.Author)
            .NotEmpty().WithMessage("El autor del comentario es obligatorio.");

        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("El mensaje del comentario es obligatorio.")
            .MaximumLength(500).WithMessage("El comentario es demasiado largo.");
    }
}
