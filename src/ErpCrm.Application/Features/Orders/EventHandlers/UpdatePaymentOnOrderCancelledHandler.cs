using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Domain.Enums;
using ErpCrm.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Orders.EventHandlers;

public class UpdatePaymentOnOrderCancelledHandler : INotificationHandler<OrderCancelledEvent>
{
    private readonly IAppDbContext _context;

    public UpdatePaymentOnOrderCancelledHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(OrderCancelledEvent notification, CancellationToken cancellationToken)
    {
        var payment = await _context.Payments
            .FirstOrDefaultAsync(x => x.OrderId == notification.OrderId, cancellationToken);

        if (payment is null)
            return;

        payment.Status = PaymentStatus.Refunded;
        payment.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
    }
}