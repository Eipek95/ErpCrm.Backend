using ErpCrm.Application.Features.StockMovements.Commands.CreateStockMovement;
using ErpCrm.Application.Features.StockMovements.Commands.DeleteStockMovement;
using ErpCrm.Application.Features.StockMovements.Commands.UpdateStockMovement;
using ErpCrm.Application.Features.StockMovements.Queries.GetStockMovementById;
using ErpCrm.Application.Features.StockMovements.Queries.GetStockMovements;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ErpCrm.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockMovementsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StockMovementsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] GetStockMovementsQuery query)
    {
        var result = await _mediator.Send(query);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetStockMovementByIdQuery(id));
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockMovementCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateStockMovementCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteStockMovementCommand(id));
        return StatusCode(result.StatusCode, result);
    }
}
