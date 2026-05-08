using ErpCrm.Application.Features.Categories.Commands.CreateCategory;
using ErpCrm.Application.Features.Categories.Commands.DeleteCategory;
using ErpCrm.Application.Features.Categories.Commands.UpdateCategory;
using ErpCrm.Application.Features.Categories.Queries.GetCategoryById;
using ErpCrm.Application.Features.Categories.Queries.GetCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace ErpCrm.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;
    public CategoriesController(IMediator mediator) => _mediator = mediator;
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] GetCategoriesQuery query)
    {
        var r = await _mediator.Send(query);
        return StatusCode(r.StatusCode, r);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var r = await _mediator.Send(new GetCategoryByIdQuery(id));
        return StatusCode(r.StatusCode, r);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
    {
        var r = await _mediator.Send(command);
        return StatusCode(r.StatusCode, r);
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryCommand command)
    {
        command.Id = id;
        var r = await _mediator.Send(command);
        return StatusCode(r.StatusCode, r);
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var r = await _mediator.Send(new DeleteCategoryCommand(id));
        return StatusCode(r.StatusCode, r);
    }
}
