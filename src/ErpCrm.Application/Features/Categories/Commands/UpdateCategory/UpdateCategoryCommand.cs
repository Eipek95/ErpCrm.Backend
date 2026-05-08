using ErpCrm.Application.Common.Results;
using MediatR;
namespace ErpCrm.Application.Features.Categories.Commands.UpdateCategory;
public class UpdateCategoryCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
