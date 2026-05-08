using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.StockMovements.Commands.UpdateStockMovement;

public class UpdateStockMovementCommandHandler : IRequestHandler<UpdateStockMovementCommand, Result<bool>>
{
    private readonly IAppDbContext _context;

    public UpdateStockMovementCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(UpdateStockMovementCommand request, CancellationToken cancellationToken)
    {
        var stockmovement = await _context.StockMovements.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (stockmovement is null)
            return Result<bool>.NotFound("StockMovement not found");

        var entity = stockmovement;

        entity.MovementType = (ErpCrm.Domain.Enums.StockMovementType)request.MovementType;
        entity.Quantity = request.Quantity;
        entity.ReferenceNumber = request.ReferenceNumber;
        entity.Description = request.Description;
        entity.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Ok(true, "StockMovement updated successfully");
    }
}
