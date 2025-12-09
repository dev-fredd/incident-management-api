using Incident.Application.DTOs;
using MediatR;

namespace Incident.Application.Queries.GetAllIncidents;

public record GetAllIncidentsQuery() : IRequest<List<IncidentDto>>;
