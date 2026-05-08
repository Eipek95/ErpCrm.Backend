using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Domain.Enums;
using ErpCrm.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Payments.EventHandlers;

public class UpdateOrderOnPaymentCompletedHandler
    : INotificationHandler<PaymentCompletedEvent>
{
    private readonly IAppDbContext _context;

    public UpdateOrderOnPaymentCompletedHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        PaymentCompletedEvent notification,
        CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(x => x.Id == notification.OrderId, cancellationToken);

        if (order is null)
            return;

        if (order.Status != OrderStatus.Cancelled)
        {
            order.Status = OrderStatus.Completed;
            order.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}