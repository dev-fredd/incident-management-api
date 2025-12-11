namespace Incident.Domain.Entities;

public class CommentEntity
{

    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid IncidentId { get; private set; }
    public string Author { get; private set; }
    public string Message { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public CommentEntity(Guid incidentId, string author, string message)
    {
        Id = Guid.NewGuid();
        IncidentId = incidentId;
        Author = author;
        Message = message;
    }

}
