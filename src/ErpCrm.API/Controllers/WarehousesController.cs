using ErpCrm.Application.Features.Warehouses.Commands.CreateWarehouse;
using ErpCrm.Application.Features.Warehouses.Commands.DeleteWarehouse;
using ErpCrm.Application.Features.Warehouses.Commands.UpdateWarehouse;
using ErpCrm.Application.Features.Warehouses.Queries.GetWarehouseById;
using ErpCrm.Application.Features.Warehouses.Queries.GetWarehouses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace ErpCrm.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WarehousesController : ControllerBase
{
    private readonly IMediator _mediator;
    public WarehousesController(IMediator mediator) => _mediator = mediator;
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] GetWarehousesQuery query)
    {
        var r = await _mediator.Send(query);
        return StatusCode(r.StatusCode, r);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var r = await _mediator.Send(new GetWarehouseByIdQuery(id));
        return StatusCode(r.StatusCode, r);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateWarehouseCommand command)
    {
        var r = await _mediator.Send(command);
        return StatusCode(r.StatusCode, r);
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateWarehouseCommand command)
    {
        command.Id = id;
        var r = await _mediator.Send(command);
        return StatusCode(r.StatusCode, r);
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var r = await _mediator.Send(new DeleteWarehouseCommand(id));
        return StatusCode(r.StatusCode, r);
    }
}
