using ErpCrm.Application.Features.ProductVariants.Commands.CreateProductVariant;
using ErpCrm.Application.Features.ProductVariants.Commands.DeleteProductVariant;
using ErpCrm.Application.Features.ProductVariants.Commands.UpdateProductVariant;
using ErpCrm.Application.Features.ProductVariants.Queries.GetProductVariantById;
using ErpCrm.Application.Features.ProductVariants.Queries.GetProductVariants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpCrm.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductVariantsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductVariantsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] GetProductVariantsQuery query)
    {
        var result = await _mediator.Send(query);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetProductVariantByIdQuery(id));
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductVariantCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductVariantCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteProductVariantCommand(id));
        return StatusCode(result.StatusCode, result);
    }
}
