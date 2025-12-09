using Incident.Domain.Entities;

namespace Incident.Application.DTOs;

public record CommentDto(
    Guid Id,
    string Author,
    string Message,
    DateTime CreatedAt
)
{
    public static CommentDto FromEntity(CommentEntity e)
        => new(e.Id, e.Author, e.Message, e.CreatedAt);
}
