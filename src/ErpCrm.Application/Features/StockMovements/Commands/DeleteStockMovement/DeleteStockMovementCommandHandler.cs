using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.StockMovements.Commands.DeleteStockMovement;

public class DeleteStockMovementCommandHandler : IRequestHandler<DeleteStockMovementCommand, Result<bool>>
{
    private readonly IAppDbContext _context;

    public DeleteStockMovementCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(DeleteStockMovementCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.StockMovements.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity is null)
            return Result<bool>.NotFound("StockMovement not found");

        entity.IsDeleted = true;
        entity.DeletedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Ok(true, "StockMovement deleted successfully");
    }
}
