using Incident.Domain.Enums;
using Incident.Domain.Interfaces;
using MediatR;

namespace Incident.Application.Commands.UpdateIncident;

public class UpdateIncidentHandler : IRequestHandler<UpdateIncidentCommand, bool>
{
    private readonly IIncidentRepository _repo;

    public UpdateIncidentHandler(IIncidentRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(UpdateIncidentCommand request, CancellationToken ct)
    {

        var incident = await _repo.GetByIdAsync(request.Id);
        if (incident == null) return false;
        Console.WriteLine($"incident {incident}");
        incident.Update(request.Title, request.Description);
        incident.ChangeStatus(request.Status);

        await _repo.UpdateAsync(incident);
        await _repo.SaveChangesAsync();

        return true;
    }
}
