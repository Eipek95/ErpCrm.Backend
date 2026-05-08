using ErpCrm.Application.Features.ProductImages.Commands.CreateProductImage;
using ErpCrm.Application.Features.ProductImages.Commands.DeleteProductImage;
using ErpCrm.Application.Features.ProductImages.Commands.UpdateProductImage;
using ErpCrm.Application.Features.ProductImages.Queries.GetProductImageById;
using ErpCrm.Application.Features.ProductImages.Queries.GetProductImages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpCrm.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductImagesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductImagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] GetProductImagesQuery query)
    {
        var result = await _mediator.Send(query);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetProductImageByIdQuery(id));
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductImageCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductImageCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteProductImageCommand(id));
        return StatusCode(result.StatusCode, result);
    }
}
