using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Stocks.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Stocks.Queries.GetStocks;

public class GetStocksQueryHandler : IRequestHandler<GetStocksQuery, Result<PagedResult<StockDto>>>
{
    private readonly IAppDbContext _context;

    public GetStocksQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PagedResult<StockDto>>> Handle(GetStocksQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Stocks.AsNoTracking().AsQueryable();

        if (request.ProductId.HasValue)
            query = query.Where(x => x.ProductId == request.ProductId.Value);

        if (request.WarehouseId.HasValue)
            query = query.Where(x => x.WarehouseId == request.WarehouseId.Value);

        if (request.ProductVariantId.HasValue)
            query = query.Where(x => x.ProductVariantId == request.ProductVariantId.Value);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            query = query.Where(x =>
                x.Product.Name.ToLower().Contains(search) ||
                x.Product.SKU.ToLower().Contains(search) ||
                x.Warehouse.Name.ToLower().Contains(search));
        }

        query = request.SortBy?.ToLower() switch
        {
            "quantity" => request.SortDescending ? query.OrderByDescending(x => x.Quantity) : query.OrderBy(x => x.Quantity),
            "reservedquantity" => request.SortDescending ? query.OrderByDescending(x => x.ReservedQuantity) : query.OrderBy(x => x.ReservedQuantity),
            _ => query.OrderByDescending(x => x.CreatedDate)
        };

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip(request.Skip)
            .Take(request.PageSize)
            .Select(x => new StockDto
            {
                Id = x.Id,
                ProductId = x.ProductId,
                ProductName = x.Product.Name,
                ProductVariantId = x.ProductVariantId,
                VariantCode = x.ProductVariant != null ? x.ProductVariant.VariantCode : null,
                WarehouseId = x.WarehouseId,
                WarehouseName = x.Warehouse.Name,
                Quantity = x.Quantity,
                ReservedQuantity = x.ReservedQuantity,
                AvailableQuantity = x.Quantity - x.ReservedQuantity
            })
            .ToListAsync(cancellationToken);

        return Result<PagedResult<StockDto>>.Ok(new PagedResult<StockDto>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        });
    }
}
