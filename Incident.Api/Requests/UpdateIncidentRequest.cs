using Incident.Domain.Enums;

namespace Incident.Api.Requests;

public class UpdateIncidentRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IncidentStatus Status { get; set; }
}
