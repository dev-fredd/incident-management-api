using Incident.Application.DTOs;
using Incident.Domain.Interfaces;
using MediatR;

namespace Incident.Application.Queries.GetAllIncidents;

public class GetAllIncidentsHandler : IRequestHandler<GetAllIncidentsQuery, List<IncidentDto>>
{
    private readonly IIncidentRepository _repo;

    public GetAllIncidentsHandler(IIncidentRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<IncidentDto>> Handle(GetAllIncidentsQuery request, CancellationToken ct)
    {
        var list = await _repo.GetAllAsync();
        return list.Select(IncidentDto.FromEntity).ToList();
    }
}
