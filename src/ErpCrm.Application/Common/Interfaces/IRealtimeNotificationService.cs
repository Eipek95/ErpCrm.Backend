namespace ErpCrm.Application.Common.Interfaces;

public interface IRealtimeNotificationService
{
    Task SendNotificationAsync(
        string title,
        string message,
        CancellationToken cancellationToken = default);

    Task SendOrderCreatedAsync(
        string orderNumber,
        decimal totalAmount,
        CancellationToken cancellationToken = default);

    Task SendLowStockAlertAsync(
        string productName,
        int remainingQuantity,
        CancellationToken cancellationToken = default);
}