using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.ProductImages.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.ProductImages.Queries.GetProductImages;

public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQuery, Result<PagedResult<ProductImageDto>>>
{
    private readonly IAppDbContext _context;

    public GetProductImagesQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PagedResult<ProductImageDto>>> Handle(GetProductImagesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.ProductImages.AsNoTracking().AsQueryable();

        if (request.ProductId.HasValue)
            query = query.Where(x => x.ProductId == request.ProductId.Value);

        if (request.ProductVariantId.HasValue)
            query = query.Where(x => x.ProductVariantId == request.ProductVariantId.Value);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            query = query.Where(x =>
                x.ImageUrl.ToLower().Contains(search) ||
                (x.AltText != null && x.AltText.ToLower().Contains(search)));
        }

        query = request.SortBy?.ToLower() switch
        {
            "sortorder" => request.SortDescending ? query.OrderByDescending(x => x.SortOrder) : query.OrderBy(x => x.SortOrder),
            "createddate" => request.SortDescending ? query.OrderByDescending(x => x.CreatedDate) : query.OrderBy(x => x.CreatedDate),
            _ => query.OrderBy(x => x.SortOrder).ThenByDescending(x => x.IsMain)
        };

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip(request.Skip)
            .Take(request.PageSize)
            .Select(x => new ProductImageDto
            {
                Id = x.Id,
                ProductId = x.ProductId,
                ProductVariantId = x.ProductVariantId,
                ImageUrl = x.ImageUrl,
                AltText = x.AltText,
                IsMain = x.IsMain,
                SortOrder = x.SortOrder
            })
            .ToListAsync(cancellationToken);

        return Result<PagedResult<ProductImageDto>>.Ok(new PagedResult<ProductImageDto>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        });
    }
}
