using ErpCrm.API.Hubs;
using ErpCrm.Application.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ErpCrm.Infrastructure.Realtime;

public class SignalRNotificationService : IRealtimeNotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public SignalRNotificationService(
        IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendNotificationAsync(
        string title,
        string message,
        CancellationToken cancellationToken = default)
    {
        await _hubContext.Clients.All.SendAsync(
            "NotificationReceived",
            new
            {
                title,
                message,
                createdDate = DateTime.UtcNow
            },
            cancellationToken);
    }

    public async Task SendOrderCreatedAsync(
        string orderNumber,
        decimal totalAmount,
        CancellationToken cancellationToken = default)
    {
        await _hubContext.Clients.All.SendAsync(
            "OrderCreated",
            new
            {
                orderNumber,
                totalAmount,
                createdDate = DateTime.UtcNow
            },
            cancellationToken);
    }

    public async Task SendLowStockAlertAsync(
        string productName,
        int remainingQuantity,
        CancellationToken cancellationToken = default)
    {
        await _hubContext.Clients.All.SendAsync(
            "LowStockAlert",
            new
            {
                productName,
                remainingQuantity,
                createdDate = DateTime.UtcNow
            },
            cancellationToken);
    }
}