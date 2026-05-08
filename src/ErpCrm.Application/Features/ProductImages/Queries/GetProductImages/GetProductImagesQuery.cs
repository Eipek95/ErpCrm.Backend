using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.ProductImages.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.ProductImages.Queries.GetProductImages;

public class GetProductImagesQuery : PagedRequest, IRequest<Result<PagedResult<ProductImageDto>>>
{
    public int? ProductId { get; set; }
    public int? ProductVariantId { get; set; }
}
