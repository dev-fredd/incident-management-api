using Incident.Application.DTOs;
using Incident.Domain.Interfaces;
using Incident.Application.Queries;
using MediatR;

namespace Incident.Application.Queries.GetIncidentById;

public class GetIncidentByIdHandler : IRequestHandler<GetIncidentByIdQuery, IncidentDto>
{
    private readonly IIncidentRepository _repo;

    public GetIncidentByIdHandler(IIncidentRepository repo)
    {
        _repo = repo;
    }

    public async Task<IncidentDto> Handle(GetIncidentByIdQuery request, CancellationToken ct)
    {
        var incident = await _repo.GetByIdAsync(request.Id);
        if (incident == null) return null;

        return IncidentDto.FromEntity(incident);
    }
}
