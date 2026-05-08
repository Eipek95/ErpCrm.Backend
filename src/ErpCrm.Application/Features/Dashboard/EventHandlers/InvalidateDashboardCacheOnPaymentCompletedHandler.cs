using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Domain.Events;
using MediatR;

namespace ErpCrm.Application.Features.Dashboard.EventHandlers;

public class InvalidateDashboardCacheOnPaymentCompletedHandler
    : INotificationHandler<PaymentCompletedEvent>
{
    private readonly ICacheService _cacheService;

    public InvalidateDashboardCacheOnPaymentCompletedHandler(
        ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task Handle(
        PaymentCompletedEvent notification,
        CancellationToken cancellationToken)
    {
        await _cacheService.RemoveAsync("dashboard:stats");
        await _cacheService.RemoveAsync($"dashboard:monthly-sales:{DateTime.UtcNow.Year}");
        await _cacheService.RemoveAsync("dashboard:top-products:10");
        await _cacheService.RemoveAsync("dashboard:warehouse-stock");
        await _cacheService.RemoveAsync("dashboard:recent-orders:10");
    }
}