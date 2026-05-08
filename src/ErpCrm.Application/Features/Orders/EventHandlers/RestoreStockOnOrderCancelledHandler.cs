using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Domain.Entities;
using ErpCrm.Domain.Enums;
using ErpCrm.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Orders.EventHandlers;

public class RestoreStockOnOrderCancelledHandler : INotificationHandler<OrderCancelledEvent>
{
    private readonly IAppDbContext _context;

    public RestoreStockOnOrderCancelledHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(OrderCancelledEvent notification, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == notification.OrderId, cancellationToken);

        if (order is null)
            return;

        foreach (var item in order.Items)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x =>
                x.ProductId == item.ProductId &&
                x.ProductVariantId == item.ProductVariantId,
                cancellationToken);

            if (stock is null)
                continue;

            stock.Quantity += item.Quantity;
            stock.UpdatedDate = DateTime.UtcNow;

            await _context.StockMovements.AddAsync(new StockMovement
            {
                ProductId = item.ProductId,
                WarehouseId = stock.WarehouseId,
                MovementType = StockMovementType.In,
                Quantity = item.Quantity,
                ReferenceNumber = notification.OrderNumber,
                Description = "Stock restored by order cancellation",
                CreatedDate = DateTime.UtcNow
            }, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}