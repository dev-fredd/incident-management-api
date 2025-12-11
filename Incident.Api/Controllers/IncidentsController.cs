using Incident.Application.Commands.CreateIncident;
using Incident.Application.Commands.UpdateIncident;
using Incident.Application.Commands.DeleteIncident;
using Incident.Application.Commands.AddComment;
using Incident.Application.Queries.GetIncidentById;
using Incident.Application.Queries.GetAllIncidents;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Incident.Api.Requests;

namespace Incident.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncidentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public IncidentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateIncident([FromBody] CreateIncidentCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetIncidentById), new { id }, new { id });
    }


    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetIncidentById(Guid id)
    {
        var incident = await _mediator.Send(new GetIncidentByIdQuery(id));
        if (incident == null) return NotFound();
        return Ok(incident);
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var incidents = await _mediator.Send(new GetAllIncidentsQuery());
        return Ok(incidents);
    }


    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateIncident(Guid id, [FromBody] UpdateIncidentRequest request)
    {
        var command = new UpdateIncidentCommand(id,request.Title,request.Description,request.Status);
        
        var result = await _mediator.Send(command);
        return result ? Ok() : NotFound();
    }


    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteIncident(Guid id)
    {
        var result = await _mediator.Send(new DeleteIncidentCommand(id));
        return result ? NoContent() : NotFound();
    }


    [HttpPost("{id:guid}/comments")]
    public async Task<IActionResult> AddComment(Guid id, [FromBody] AddCommentRequest request)
    {
        var command  = new AddCommentCommand(id,request.Author,request.Message);
        var result = await _mediator.Send(command);
        return result ? Ok() : NotFound();
    }
}
