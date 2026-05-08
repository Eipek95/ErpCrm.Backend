using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.StockMovements.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.StockMovements.Queries.GetStockMovements;

public class GetStockMovementsQueryHandler : IRequestHandler<GetStockMovementsQuery, Result<PagedResult<StockMovementDto>>>
{
    private readonly IAppDbContext _context;

    public GetStockMovementsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PagedResult<StockMovementDto>>> Handle(GetStockMovementsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.StockMovements.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            query = query.Where(x => (x.ReferenceNumber != null && x.ReferenceNumber.ToLower().Contains(search)) || (x.Description != null && x.Description.ToLower().Contains(search)));
        }

        query = request.SortBy?.ToLower() switch
        {
            "quantity" => request.SortDescending ? query.OrderByDescending(x => x.Quantity) : query.OrderBy(x => x.Quantity),
            "createddate" => request.SortDescending ? query.OrderByDescending(x => x.CreatedDate) : query.OrderBy(x => x.CreatedDate),
            _ => query.OrderByDescending(x => x.CreatedDate)
        };

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip(request.Skip)
            .Take(request.PageSize)
            .Select(x => new StockMovementDto
            {
                Id = x.Id,
                ProductId = x.ProductId,
                WarehouseId = x.WarehouseId,
                MovementType = (int)x.MovementType,
                Quantity = x.Quantity,
                ReferenceNumber = x.ReferenceNumber,
                Description = x.Description,
                CreatedDate = x.CreatedDate
            })
            .ToListAsync(cancellationToken);

        return Result<PagedResult<StockMovementDto>>.Ok(new PagedResult<StockMovementDto>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        });
    }
}
