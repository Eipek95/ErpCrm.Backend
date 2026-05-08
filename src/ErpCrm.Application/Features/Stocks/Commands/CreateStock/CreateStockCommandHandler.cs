using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Domain.Entities;
using ErpCrm.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Stocks.Commands.CreateStock;

public class CreateStockCommandHandler : IRequestHandler<CreateStockCommand, Result<int>>
{
    private readonly IAppDbContext _context;

    public CreateStockCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(CreateStockCommand request, CancellationToken cancellationToken)
    {
        if (request.Quantity < 0 || request.ReservedQuantity < 0)
            return Result<int>.Fail("Stock quantities cannot be negative");

        if (request.ReservedQuantity > request.Quantity)
            return Result<int>.Fail("Reserved quantity cannot be greater than quantity");

        var productExists = await _context.Products.AnyAsync(x => x.Id == request.ProductId, cancellationToken);
        if (!productExists)
            return Result<int>.Fail("Product not found");

        var warehouseExists = await _context.Warehouses.AnyAsync(x => x.Id == request.WarehouseId, cancellationToken);
        if (!warehouseExists)
            return Result<int>.Fail("Warehouse not found");

        if (request.ProductVariantId.HasValue)
        {
            var variantExists = await _context.ProductVariants.AnyAsync(x =>
                x.Id == request.ProductVariantId.Value &&
                x.ProductId == request.ProductId, cancellationToken);

            if (!variantExists)
                return Result<int>.Fail("Product variant not found");
        }

        var exists = await _context.Stocks.AnyAsync(x =>
            x.ProductId == request.ProductId &&
            x.ProductVariantId == request.ProductVariantId &&
            x.WarehouseId == request.WarehouseId, cancellationToken);

        if (exists)
            return Result<int>.Fail("Stock record already exists for this product/variant/warehouse", 409);

        var stock = new Stock
        {
            ProductId = request.ProductId,
            ProductVariantId = request.ProductVariantId,
            WarehouseId = request.WarehouseId,
            Quantity = request.Quantity,
            ReservedQuantity = request.ReservedQuantity,
            CreatedDate = DateTime.UtcNow
        };

        await _context.Stocks.AddAsync(stock, cancellationToken);

        await _context.StockMovements.AddAsync(new StockMovement
        {
            ProductId = request.ProductId,
            WarehouseId = request.WarehouseId,
            MovementType = StockMovementType.In,
            Quantity = request.Quantity,
            ReferenceNumber = $"INIT-{DateTime.UtcNow:yyyyMMddHHmmss}",
            Description = "Initial stock creation",
            CreatedDate = DateTime.UtcNow
        }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<int>.Created(stock.Id, "Stock created successfully");
    }
}
