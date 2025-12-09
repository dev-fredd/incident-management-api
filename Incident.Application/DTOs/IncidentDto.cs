using Incident.Domain.Entities;

namespace Incident.Application.DTOs;

public record IncidentDto(
    Guid Id,
    string Title,
    string Description,
    string Status,
    Guid UserId,
    Guid CategoryId,
    List<CommentDto> Comments
)
{
    public static IncidentDto FromEntity(IncidentEntity e)
        => new(e.Id, e.Title, e.Description, e.Status.ToString(),
               e.UserId, e.CategoryId,
               e.Comments.Select(CommentDto.FromEntity).ToList());
}
