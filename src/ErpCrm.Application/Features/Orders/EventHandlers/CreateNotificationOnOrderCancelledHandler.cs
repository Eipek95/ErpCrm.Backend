using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Domain.Entities;
using ErpCrm.Domain.Events;
using MediatR;

namespace ErpCrm.Application.Features.Orders.EventHandlers;

public class CreateNotificationOnOrderCancelledHandler
    : INotificationHandler<OrderCancelledEvent>
{
    private readonly IAppDbContext _context;

    public CreateNotificationOnOrderCancelledHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        OrderCancelledEvent notification,
        CancellationToken cancellationToken)
    {
        await _context.Notifications.AddAsync(new Notification
        {
            UserId = notification.UserId,
            Title = "Order cancelled",
            Message = $"{notification.OrderNumber} order has been cancelled.",
            IsRead = false,
            CreatedDate = DateTime.UtcNow
        }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
}