using ErpCrm.Application.Features.Stocks.Commands.CreateStock;
using ErpCrm.Application.Features.Stocks.Commands.DeleteStock;
using ErpCrm.Application.Features.Stocks.Commands.UpdateStock;
using ErpCrm.Application.Features.Stocks.Queries.GetStockById;
using ErpCrm.Application.Features.Stocks.Queries.GetStocks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ErpCrm.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StocksController : ControllerBase
{
    private readonly IMediator _mediator;

    public StocksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] GetStocksQuery query)
    {
        var result = await _mediator.Send(query);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetStockByIdQuery(id));
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateStockCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteStockCommand(id));
        return StatusCode(result.StatusCode, result);
    }
}
