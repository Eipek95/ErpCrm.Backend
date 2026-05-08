using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Domain.Entities;
using ErpCrm.Domain.Events;
using MediatR;

namespace ErpCrm.Application.Features.Orders.EventHandlers;

public class CreateAuditLogOnOrderCancelledHandler
    : INotificationHandler<OrderCancelledEvent>
{
    private readonly IAppDbContext _context;

    public CreateAuditLogOnOrderCancelledHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        OrderCancelledEvent notification,
        CancellationToken cancellationToken)
    {
        await _context.AuditLogs.AddAsync(new AuditLog
        {
            UserId = notification.UserId,
            Action = "OrderCancelled",
            EntityName = "Order",
            NewValues = $"OrderId: {notification.OrderId}, OrderNumber: {notification.OrderNumber}",
            CreatedDate = DateTime.UtcNow
        }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
}