using ErpCrm.Application.Common.Constants;
using ErpCrm.Application.Features.AuditLogs.Commands.CreateAuditLog;
using ErpCrm.Application.Features.AuditLogs.Commands.DeleteAuditLog;
using ErpCrm.Application.Features.AuditLogs.Commands.UpdateAuditLog;
using ErpCrm.Application.Features.AuditLogs.Queries.GetAuditLogById;
using ErpCrm.Application.Features.AuditLogs.Queries.GetAuditLogs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ErpCrm.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = AuthorizationPolicies.AdminOnly)]
public class AuditLogsController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuditLogsController(IMediator mediator) => _mediator = mediator;
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] GetAuditLogsQuery query)
    {
        var r = await _mediator.Send(query);
        return StatusCode(r.StatusCode, r);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var r = await _mediator.Send(new GetAuditLogByIdQuery(id));
        return StatusCode(r.StatusCode, r);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAuditLogCommand command)
    {
        var r = await _mediator.Send(command);
        return StatusCode(r.StatusCode, r);
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAuditLogCommand command)
    {
        command.Id = id;
        var r = await _mediator.Send(command);
        return StatusCode(r.StatusCode, r);
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var r = await _mediator.Send(new DeleteAuditLogCommand(id));
        return StatusCode(r.StatusCode, r);
    }
}
