using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Stocks.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Stocks.Queries.GetStockById;

public class GetStockByIdQueryHandler : IRequestHandler<GetStockByIdQuery, Result<StockDto>>
{
    private readonly IAppDbContext _context;

    public GetStockByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<StockDto>> Handle(GetStockByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _context.Stocks
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
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
            .FirstOrDefaultAsync(cancellationToken);

        return item is null
            ? Result<StockDto>.NotFound("Stock not found")
            : Result<StockDto>.Ok(item);
    }
}
