using Incident.Domain.Interfaces;
using MediatR;

namespace Incident.Application.Commands.AddComment;

public class AddCommentHandler : IRequestHandler<AddCommentCommand, bool>
{
    private readonly IIncidentRepository _repo;

    public AddCommentHandler(IIncidentRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(AddCommentCommand request, CancellationToken ct)
    {
        var incident = await _repo.GetByIdAsync(request.IncidentId);
        if (incident == null) return false;

        incident.AddComment(request.Author, request.Message);

        await _repo.UpdateAsync(incident);
        await _repo.SaveChangesAsync();

        return true;
    }
}
