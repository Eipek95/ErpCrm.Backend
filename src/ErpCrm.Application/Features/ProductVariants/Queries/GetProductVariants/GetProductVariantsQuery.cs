using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.ProductVariants.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.ProductVariants.Queries.GetProductVariants;

public class GetProductVariantsQuery : PagedRequest, IRequest<Result<PagedResult<ProductVariantDto>>>
{
    public int? ProductId { get; set; }
    public bool? IsActive { get; set; }
}
