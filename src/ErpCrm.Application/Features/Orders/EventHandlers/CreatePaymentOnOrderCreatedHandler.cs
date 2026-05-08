using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Domain.Entities;
using ErpCrm.Domain.Enums;
using ErpCrm.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ErpCrm.Application.Features.Orders.EventHandlers;

public class CreatePaymentOnOrderCreatedHandler : INotificationHandler<OrderCreatedEvent>
{
    private readonly IAppDbContext _context;
    private readonly ILogger<CreatePaymentOnOrderCreatedHandler> _logger;

    public CreatePaymentOnOrderCreatedHandler(
        IAppDbContext context,
        ILogger<CreatePaymentOnOrderCreatedHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(x => x.OrderNumber == notification.OrderNumber, cancellationToken);

        if (order is null)
        {
            _logger.LogWarning(
                "Order not found while creating payment. OrderNumber: {OrderNumber}",
                notification.OrderNumber);

            return;
        }

        var paymentExists = await _context.Payments
            .AnyAsync(x => x.OrderId == order.Id, cancellationToken);

        if (paymentExists)
            return;

        await _context.Payments.AddAsync(new Payment
        {
            OrderId = order.Id,
            Amount = notification.TotalAmount,
            Method = PaymentMethod.CreditCard,
            Status = PaymentStatus.Paid,
            PaidDate = DateTime.UtcNow,
            CreatedDate = DateTime.UtcNow
        }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Payment created for order. OrderNumber: {OrderNumber}",
            notification.OrderNumber);
    }
}
