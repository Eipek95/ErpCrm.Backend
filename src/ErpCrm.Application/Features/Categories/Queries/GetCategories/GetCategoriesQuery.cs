using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Categories.DTOs;
using MediatR;
namespace ErpCrm.Application.Features.Categories.Queries.GetCategories;
public class GetCategoriesQuery : PagedRequest, IRequest<Result<PagedResult<CategoryDto>>>
{

}
