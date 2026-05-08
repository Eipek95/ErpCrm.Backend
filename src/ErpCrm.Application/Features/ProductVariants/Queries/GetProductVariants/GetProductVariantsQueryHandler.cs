using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.ProductVariants.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.ProductVariants.Queries.GetProductVariants;

public class GetProductVariantsQueryHandler : IRequestHandler<GetProductVariantsQuery, Result<PagedResult<ProductVariantDto>>>
{
    private readonly IAppDbContext _context;

    public GetProductVariantsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PagedResult<ProductVariantDto>>> Handle(GetProductVariantsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.ProductVariants.AsNoTracking().AsQueryable();

        if (request.ProductId.HasValue)
            query = query.Where(x => x.ProductId == request.ProductId.Value);

        if (request.IsActive.HasValue)
            query = query.Where(x => x.IsActive == request.IsActive.Value);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            query = query.Where(x =>
                x.VariantCode.ToLower().Contains(search) ||
                (x.Color != null && x.Color.ToLower().Contains(search)) ||
                (x.Size != null && x.Size.ToLower().Contains(search)) ||
                x.Product.Name.ToLower().Contains(search));
        }

        query = request.SortBy?.ToLower() switch
        {
            "variantcode" => request.SortDescending ? query.OrderByDescending(x => x.VariantCode) : query.OrderBy(x => x.VariantCode),
            "price" => request.SortDescending ? query.OrderByDescending(x => x.Price) : query.OrderBy(x => x.Price),
            "createddate" => request.SortDescending ? query.OrderByDescending(x => x.CreatedDate) : query.OrderBy(x => x.CreatedDate),
            _ => query.OrderByDescending(x => x.CreatedDate)
        };

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip(request.Skip)
            .Take(request.PageSize)
            .Select(x => new ProductVariantDto
            {
                Id = x.Id,
                ProductId = x.ProductId,
                ProductName = x.Product.Name,
                VariantCode = x.VariantCode,
                Color = x.Color,
                Size = x.Size,
                Price = x.Price,
                StockQuantity = x.StockQuantity,
                IsActive = x.IsActive,
                CreatedDate = x.CreatedDate
            })
            .ToListAsync(cancellationToken);

        return Result<PagedResult<ProductVariantDto>>.Ok(new PagedResult<ProductVariantDto>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        });
    }
}
