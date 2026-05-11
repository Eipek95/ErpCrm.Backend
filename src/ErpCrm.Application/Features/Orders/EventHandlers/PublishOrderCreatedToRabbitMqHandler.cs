using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Features.Orders.Messages;
using ErpCrm.Domain.Events;
using MediatR;

namespace ErpCrm.Application.Features.Orders.EventHandlers;

public class PublishOrderCreatedToRabbitMqHandler
    : INotificationHandler<OrderCreatedEvent>
{
    private readonly IMessageBusService _messageBusService;

    public PublishOrderCreatedToRabbitMqHandler(
        IMessageBusService messageBusService)
    {
        _messageBusService = messageBusService;
    }

    public async Task Handle(
        OrderCreatedEvent notification,
        CancellationToken cancellationToken)
    {
        var message = new OrderCreatedMessage
        {
            OrderNumber = notification.OrderNumber,
            CustomerId = notification.CustomerId,
            CreatedByUserId = notification.CreatedByUserId,
            TotalAmount = notification.TotalAmount,
            CreatedDate = DateTime.UtcNow
        };

        await _messageBusService.PublishAsync(
            "order-created-queue",
            message,
            cancellationToken);
    }
}