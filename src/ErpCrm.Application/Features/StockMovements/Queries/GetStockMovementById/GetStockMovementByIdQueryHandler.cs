using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.StockMovements.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.StockMovements.Queries.GetStockMovementById;

public class GetStockMovementByIdQueryHandler : IRequestHandler<GetStockMovementByIdQuery, Result<StockMovementDto>>
{
    private readonly IAppDbContext _context;

    public GetStockMovementByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<StockMovementDto>> Handle(GetStockMovementByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _context.StockMovements
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
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
            .FirstOrDefaultAsync(cancellationToken);

        return item is null
            ? Result<StockMovementDto>.NotFound("StockMovement not found")
            : Result<StockMovementDto>.Ok(item);
    }
}
