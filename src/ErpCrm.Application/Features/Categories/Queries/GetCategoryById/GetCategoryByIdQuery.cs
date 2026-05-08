using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Categories.DTOs;
using MediatR;
namespace ErpCrm.Application.Features.Categories.Queries.GetCategoryById;
public record GetCategoryByIdQuery(int Id) : IRequest<Result<CategoryDto>>;
