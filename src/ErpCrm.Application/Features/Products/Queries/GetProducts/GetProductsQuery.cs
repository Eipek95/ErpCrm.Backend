using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Products.DTOs;
using MediatR;
namespace ErpCrm.Application.Features.Products.Queries.GetProducts;
public class GetProductsQuery : PagedRequest, IRequest<Result<PagedResult<ProductDto>>>
{
    public int? CategoryId { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsPopular { get; set; }
}
