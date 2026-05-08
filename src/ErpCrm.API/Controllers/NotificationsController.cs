using ErpCrm.Application.Features.Notifications.Commands.CreateNotification;
using ErpCrm.Application.Features.Notifications.Commands.DeleteNotification;
using ErpCrm.Application.Features.Notifications.Commands.UpdateNotification;
using ErpCrm.Application.Features.Notifications.Queries.GetNotificationById;
using ErpCrm.Application.Features.Notifications.Queries.GetNotifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace ErpCrm.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly IMediator _mediator;
    public NotificationsController(IMediator mediator) => _mediator = mediator;
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] GetNotificationsQuery query)
    {
        var r = await _mediator.Send(query);
        return StatusCode(r.StatusCode, r);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var r = await _mediator.Send(new GetNotificationByIdQuery(id));
        return StatusCode(r.StatusCode, r);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNotificationCommand command)
    {
        var r = await _mediator.Send(command);
        return StatusCode(r.StatusCode, r);
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateNotificationCommand command)
    {
        command.Id = id;
        var r = await _mediator.Send(command);
        return StatusCode(r.StatusCode, r);
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var r = await _mediator.Send(new DeleteNotificationCommand(id));
        return StatusCode(r.StatusCode, r);
    }
}
