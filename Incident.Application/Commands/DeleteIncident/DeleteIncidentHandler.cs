using Incident.Domain.Interfaces;
using MediatR;

namespace Incident.Application.Commands.DeleteIncident;

public class DeleteIncidentHandler : IRequestHandler<DeleteIncidentCommand, bool>
{
    private readonly IIncidentRepository _repo;

    public DeleteIncidentHandler(IIncidentRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(DeleteIncidentCommand request, CancellationToken ct)
    {
        var incident = await _repo.GetByIdAsync(request.Id);
        if (incident == null) return false;

        await _repo.DeleteAsync(incident);
        await _repo.SaveChangesAsync();

        return true;
    }
}
