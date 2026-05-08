using ErpCrm.Application.Features.Orders.Commands.CreateOrder;
using ErpCrm.Application.Features.Orders.Commands.DeleteOrder;
using ErpCrm.Application.Features.Orders.Commands.UpdateOrder;
using ErpCrm.Application.Features.Orders.Queries.GetOrderById;
using ErpCrm.Application.Features.Orders.Queries.GetOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ErpCrm.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] GetOrdersQuery query)
    {
        var result = await _mediator.Send(query);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetOrderByIdQuery(id));
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteOrderCommand(id));
        return StatusCode(result.StatusCode, result);
    }
}
