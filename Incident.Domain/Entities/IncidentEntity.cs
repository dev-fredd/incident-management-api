
namespace Incident.Domain.Entities;

using Incident.Domain.Enums;
public class IncidentEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; }
    public string Description { get; private set; }
    public Guid UserId { get; private set; }
    public Guid CategoryId { get; private set; }
    public IncidentStatus Status { get; private set; } = IncidentStatus.Open;
    
    public List<CommentEntity> Comments { get; private set; } = new();

    public IncidentEntity(string title, string description, Guid userId, Guid categoryId)
    {
        Title = title;
        Description = description;
        UserId = userId;
        CategoryId = categoryId;
    }

    public void Update(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public void ChangeStatus(IncidentStatus status)
    {
        Status = status;
    }

    public void AddComment(string author, string message)
    {
        Comments.Add(new CommentEntity(Id, author, message));
    }
}
