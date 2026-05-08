using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Domain.Entities;
using MediatR;

namespace ErpCrm.Application.Features.StockMovements.Commands.CreateStockMovement;

public class CreateStockMovementCommandHandler : IRequestHandler<CreateStockMovementCommand, Result<int>>
{
    private readonly IAppDbContext _context;

    public CreateStockMovementCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(CreateStockMovementCommand request, CancellationToken cancellationToken)
    {
        var entity = new StockMovement
        {
            ProductId = request.ProductId,
            WarehouseId = request.WarehouseId,
            MovementType = (ErpCrm.Domain.Enums.StockMovementType)request.MovementType,
            Quantity = request.Quantity,
            ReferenceNumber = request.ReferenceNumber,
            Description = request.Description,
            CreatedDate = DateTime.UtcNow
        };

        await _context.StockMovements.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<int>.Created(entity.Id, "StockMovement created successfully");
    }
}
