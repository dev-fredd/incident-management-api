using MediatR;

namespace Incident.Application.Commands.DeleteIncident;

public record DeleteIncidentCommand(Guid Id) : IRequest<bool>;
