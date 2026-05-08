using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Domain.Entities;
using ErpCrm.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ErpCrm.Application.Features.Orders.EventHandlers;

public class CreateAuditLogOnOrderCreatedHandler : INotificationHandler<OrderCreatedEvent>
{
    private readonly IAppDbContext _context;
    private readonly ILogger<CreateAuditLogOnOrderCreatedHandler> _logger;

    public CreateAuditLogOnOrderCreatedHandler(
        IAppDbContext context,
        ILogger<CreateAuditLogOnOrderCreatedHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _context.AuditLogs.AddAsync(new AuditLog
        {
            UserId = notification.CreatedByUserId,
            Action = "OrderCreated",
            EntityName = "Order",
            NewValues = $"OrderNumber: {notification.OrderNumber}, TotalAmount: {notification.TotalAmount}",
            CreatedDate = DateTime.UtcNow
        }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Audit log created for order. OrderNumber: {OrderNumber}",
            notification.OrderNumber);
    }
}
