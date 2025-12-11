using Incident.Domain.Entities;
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
        Console.WriteLine($"incident: {request.IncidentId}");
        var incident = await _repo.GetByIdAsync(request.IncidentId);
        if (incident == null) return false;

        var comment = new CommentEntity(request.IncidentId, request.Author, request.Message);
        incident.Comments.Add(comment);
        Console.WriteLine($"incident: {incident.Description}");
        // await _repo.UpdateAsync(incident);
        await _repo.SaveChangesAsync();

        return true;
    }
}
