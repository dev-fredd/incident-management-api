using Incident.Domain.Entities;
using Incident.Domain.Interfaces;
using MediatR;

namespace Incident.Application.Commands.CreateIncident;



public class CreateIncidentHandler : IRequestHandler<CreateIncidentCommand, Guid>
{
    private readonly IIncidentRepository _repo;

    public CreateIncidentHandler(IIncidentRepository repo)
    {
        _repo = repo;
    }

    public async Task<Guid> Handle(CreateIncidentCommand request, CancellationToken ct)
    {
        var incident = new IncidentEntity(
            request.Title,
            request.Description,
            request.UserId,
            request.CategoryId
        );

        await _repo.AddAsync(incident);
        await _repo.SaveChangesAsync();

        return incident.Id;
    }
}
