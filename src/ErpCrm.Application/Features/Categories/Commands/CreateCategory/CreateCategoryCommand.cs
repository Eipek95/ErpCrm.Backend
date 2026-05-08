using ErpCrm.Application.Common.Results;
using MediatR;
namespace ErpCrm.Application.Features.Categories.Commands.CreateCategory;
public class CreateCategoryCommand : IRequest<Result<int>>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
