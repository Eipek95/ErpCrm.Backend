using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ErpCrm.Application.Features.Orders.EventHandlers;

public class CreateNotificationOnOrderCreatedHandler : INotificationHandler<OrderCreatedEvent>
{
    private readonly IAppDbContext _context;
    private readonly ILogger<CreateNotificationOnOrderCreatedHandler> _logger;

    public CreateNotificationOnOrderCreatedHandler(
        IAppDbContext context,
        ILogger<CreateNotificationOnOrderCreatedHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _context.Notifications.AddAsync(new Notification
        {
            UserId = notification.CreatedByUserId,
            Title = "New order created",
            Message = $"{notification.OrderNumber} order has been created.",
            IsRead = false,
            CreatedDate = DateTime.UtcNow
        }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Notification created for order. OrderNumber: {OrderNumber}",
            notification.OrderNumber);
    }
}
