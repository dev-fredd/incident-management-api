using MediatR;

namespace Incident.Application.Commands.AddComment;

public record AddCommentCommand(
    Guid IncidentId,
    string Author,
    string Message
) : IRequest<bool>;
