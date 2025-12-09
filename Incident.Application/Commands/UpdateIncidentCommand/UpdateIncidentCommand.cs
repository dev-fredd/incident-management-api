using Incident.Domain.Enums;
using MediatR;

namespace Incident.Application.Commands.UpdateIncident;

public record UpdateIncidentCommand(
    Guid Id,
    string Title,
    string Description,
    IncidentStatus Status
) : IRequest<bool>;
