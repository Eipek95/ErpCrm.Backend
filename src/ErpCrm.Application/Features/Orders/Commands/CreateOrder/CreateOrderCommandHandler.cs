using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Domain.Entities;
using ErpCrm.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<int>>
{
    private readonly IAppDbContext _context;

    public CreateOrderCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        if (!request.Items.Any())
            return Result<int>.Fail("Order must contain at least one item");

        var customerExists = await _context.Customers.AnyAsync(x => x.Id == request.CustomerId && x.IsActive, cancellationToken);
        if (!customerExists)
            return Result<int>.Fail("Active customer not found");

        var userExists = await _context.Users.AnyAsync(x => x.Id == request.CreatedByUserId && x.IsActive, cancellationToken);
        if (!userExists)
            return Result<int>.Fail("Active user not found");

        var order = new Order
        {
            CustomerId = request.CustomerId,
            CreatedByUserId = request.CreatedByUserId,
            OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMddHHmmssfff}",
            Status = OrderStatus.Confirmed,
            CreatedDate = DateTime.UtcNow
        };

        decimal totalAmount = 0;

        foreach (var requestItem in request.Items)
        {
            if (requestItem.Quantity <= 0)
                return Result<int>.Fail("Quantity must be greater than zero");

            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == requestItem.ProductId && x.IsActive, cancellationToken);
            if (product is null)
                return Result<int>.Fail($"Product not found: {requestItem.ProductId}");

            var stock = await _context.Stocks.FirstOrDefaultAsync(x =>
                x.ProductId == requestItem.ProductId &&
                x.ProductVariantId == requestItem.ProductVariantId &&
                x.WarehouseId == request.WarehouseId, cancellationToken);

            if (stock is null || stock.Quantity - stock.ReservedQuantity < requestItem.Quantity)
                return Result<int>.Fail($"Insufficient stock for product: {product.Name}");

            var unitPrice = product.Price;

            if (requestItem.ProductVariantId.HasValue)
            {
                var variant = await _context.ProductVariants.FirstOrDefaultAsync(x =>
                    x.Id == requestItem.ProductVariantId.Value &&
                    x.ProductId == requestItem.ProductId &&
                    x.IsActive, cancellationToken);

                if (variant is null)
                    return Result<int>.Fail($"Product variant not found: {requestItem.ProductVariantId}");

                unitPrice = variant.Price ?? product.Price;
            }

            var totalPrice = unitPrice * requestItem.Quantity;

            order.Items.Add(new OrderItem
            {
                ProductId = requestItem.ProductId,
                ProductVariantId = requestItem.ProductVariantId,
                Quantity = requestItem.Quantity,
                UnitPrice = unitPrice,
                TotalPrice = totalPrice,
                CreatedDate = DateTime.UtcNow
            });

            stock.Quantity -= requestItem.Quantity;
            stock.UpdatedDate = DateTime.UtcNow;

            await _context.StockMovements.AddAsync(new StockMovement
            {
                ProductId = requestItem.ProductId,
                WarehouseId = request.WarehouseId,
                MovementType = StockMovementType.Out,
                Quantity = requestItem.Quantity,
                ReferenceNumber = order.OrderNumber,
                Description = "Stock decreased by order creation",
                CreatedDate = DateTime.UtcNow
            }, cancellationToken);

            totalAmount += totalPrice;
        }

        order.TotalAmount = totalAmount;

        await _context.Orders.AddAsync(order, cancellationToken);

        await _context.Payments.AddAsync(new Payment
        {
            Order = order,
            Amount = totalAmount,
            Method = PaymentMethod.CreditCard,
            Status = PaymentStatus.Paid,
            PaidDate = DateTime.UtcNow,
            CreatedDate = DateTime.UtcNow
        }, cancellationToken);

        await _context.Notifications.AddAsync(new Notification
        {
            UserId = request.CreatedByUserId,
            Title = "New order created",
            Message = $"{order.OrderNumber} order has been created.",
            IsRead = false,
            CreatedDate = DateTime.UtcNow
        }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<int>.Created(order.Id, "Order created successfully");
    }
}
