using ErpCrm.Application.Features.Products.Commands.CreateProduct;
using ErpCrm.Application.Features.Products.Commands.DeleteProduct;
using ErpCrm.Application.Features.Products.Commands.UpdateProduct;
using ErpCrm.Application.Features.Products.Queries.GetProductById;
using ErpCrm.Application.Features.Products.Queries.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace ErpCrm.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProductsController(IMediator mediator) => _mediator = mediator;
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] GetProductsQuery query)
    {
        var r = await _mediator.Send(query);
        return StatusCode(r.StatusCode, r);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var r = await _mediator.Send(new GetProductByIdQuery(id));
        return StatusCode(r.StatusCode, r);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var r = await _mediator.Send(command);
        return StatusCode(r.StatusCode, r);
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductCommand command)
    {
        command.Id = id;
        var r = await _mediator.Send(command);
        return StatusCode(r.StatusCode, r);
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var r = await _mediator.Send(new DeleteProductCommand(id));
        return StatusCode(r.StatusCode, r);
    }
}
