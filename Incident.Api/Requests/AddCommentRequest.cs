using Incident.Domain.Enums;

namespace Incident.Api.Requests;

public class AddCommentRequest
{
    public string Author { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
