using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ErpCrm.Infrastructure.BackgroundJobs;

public class LowStockCheckJob
{
    private readonly IAppDbContext _context;
    private readonly ILogger<LowStockCheckJob> _logger;
    private readonly IRealtimeNotificationService _realtimeNotificationService;
    public LowStockCheckJob(
        IAppDbContext context,
        ILogger<LowStockCheckJob> logger,
        IRealtimeNotificationService realtimeNotificationService)
    {
        _context = context;
        _logger = logger;
        _realtimeNotificationService = realtimeNotificationService;
    }

    public async Task ExecuteAsync()
    {
        _logger.LogInformation("LowStockCheckJob started");

        var lowStocks = await _context.Stocks
            .AsNoTracking()
            .Include(x => x.Product)
            .Where(x => x.Quantity - x.ReservedQuantity <= 10)
            .ToListAsync();

        if (!lowStocks.Any())
        {
            _logger.LogInformation("No low stock products found");
            return;
        }

        var adminUsers = await _context.Users
            .AsNoTracking()
            .Where(x => x.IsActive)
            .Take(5)
            .ToListAsync();

        foreach (var user in adminUsers)
        {
            foreach (var stock in lowStocks)
            {
                await _context.Notifications.AddAsync(new Notification
                {
                    UserId = user.Id,
                    Title = "Low Stock Alert",
                    Message =
                        $"{stock.Product.Name} stock is critical. " +
                        $"Remaining quantity: {stock.Quantity - stock.ReservedQuantity}",
                    IsRead = false,
                    CreatedDate = DateTime.UtcNow
                });

                await _realtimeNotificationService.SendLowStockAlertAsync(
                     stock.Product.Name,
                     stock.Quantity - stock.ReservedQuantity);
            }
        }

        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "LowStockCheckJob completed. Notification count: {Count}",
            lowStocks.Count * adminUsers.Count);
    }
}