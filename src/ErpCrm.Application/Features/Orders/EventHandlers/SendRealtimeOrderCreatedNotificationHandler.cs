using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Domain.Events;
using MediatR;

namespace ErpCrm.Application.Features.Orders.EventHandlers;

public class SendRealtimeOrderCreatedNotificationHandler
    : INotificationHandler<OrderCreatedEvent>
{
    private readonly IRealtimeNotificationService _realtimeNotificationService;

    public SendRealtimeOrderCreatedNotificationHandler(
        IRealtimeNotificationService realtimeNotificationService)
    {
        _realtimeNotificationService = realtimeNotificationService;
    }

    public async Task Handle(
        OrderCreatedEvent notification,
        CancellationToken cancellationToken)
    {
        await _realtimeNotificationService.SendOrderCreatedAsync(
            notification.OrderNumber,
            notification.TotalAmount,
            cancellationToken);
    }
}