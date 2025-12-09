using MediatR;

namespace Incident.Application.Commands.CreateIncident;

public record CreateIncidentCommand(
    string Title,
    string Description,
    Guid UserId,
    Guid CategoryId
) : IRequest<Guid>;
