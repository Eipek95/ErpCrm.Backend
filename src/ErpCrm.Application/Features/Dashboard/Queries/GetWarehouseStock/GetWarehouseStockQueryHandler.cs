using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Dashboard.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Dashboard.Queries.GetWarehouseStock;

public class GetWarehouseStockQueryHandler
    : IRequestHandler<GetWarehouseStockQuery, Result<List<WarehouseStockDto>>>
{
    private readonly IAppDbContext _context;

    public GetWarehouseStockQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<WarehouseStockDto>>> Handle(
        GetWarehouseStockQuery request,
        CancellationToken cancellationToken)
    {
        var data = await _context.Stocks
            .AsNoTracking()
            .GroupBy(x => new
            {
                x.WarehouseId,
                x.Warehouse.Name
            })
            .Select(x => new WarehouseStockDto
            {
                WarehouseId = x.Key.WarehouseId,
                WarehouseName = x.Key.Name,
                TotalQuantity = x.Sum(s => s.Quantity),
                ReservedQuantity = x.Sum(s => s.ReservedQuantity),
                AvailableQuantity = x.Sum(s => s.Quantity - s.ReservedQuantity),
                ProductCount = x.Select(s => s.ProductId).Distinct().Count()
            })
            .OrderByDescending(x => x.AvailableQuantity)
            .ToListAsync(cancellationToken);

        return Result<List<WarehouseStockDto>>.Ok(data);
    }
}