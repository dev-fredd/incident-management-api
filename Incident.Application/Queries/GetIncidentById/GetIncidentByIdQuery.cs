using MediatR;
using Incident.Application.DTOs;

namespace Incident.Application.Queries.GetIncidentById;

public record GetIncidentByIdQuery(Guid Id) : IRequest<IncidentDto>;
