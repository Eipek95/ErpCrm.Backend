using ErpCrm.Application.Common.Results;
using MediatR;
namespace ErpCrm.Application.Features.Categories.Commands.DeleteCategory;
public record DeleteCategoryCommand(int Id) : IRequest<Result<bool>>;
