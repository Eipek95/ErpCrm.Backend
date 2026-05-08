using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Domain.Entities;
using ErpCrm.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Stocks.Commands.UpdateStock;

public class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand, Result<bool>>
{
    private readonly IAppDbContext _context;

    public UpdateStockCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
    {
        if (request.Quantity < 0 || request.ReservedQuantity < 0)
            return Result<bool>.Fail("Stock quantities cannot be negative");

        if (request.ReservedQuantity > request.Quantity)
            return Result<bool>.Fail("Reserved quantity cannot be greater than quantity");

        var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (stock is null)
            return Result<bool>.NotFound("Stock not found");

        var difference = request.Quantity - stock.Quantity;

        stock.Quantity = request.Quantity;
        stock.ReservedQuantity = request.ReservedQuantity;
        stock.UpdatedDate = DateTime.UtcNow;

        if (difference != 0)
        {
            await _context.StockMovements.AddAsync(new StockMovement
            {
                ProductId = stock.ProductId,
                WarehouseId = stock.WarehouseId,
                MovementType = StockMovementType.Adjustment,
                Quantity = difference,
                ReferenceNumber = $"ADJ-{DateTime.UtcNow:yyyyMMddHHmmss}",
                Description = "Stock quantity adjusted",
                CreatedDate = DateTime.UtcNow
            }, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Ok(true, "Stock updated successfully");
    }
}
