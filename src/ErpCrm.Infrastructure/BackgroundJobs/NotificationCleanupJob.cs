using ErpCrm.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ErpCrm.Infrastructure.BackgroundJobs;

public class NotificationCleanupJob
{
    private readonly IAppDbContext _context;
    private readonly ILogger<NotificationCleanupJob> _logger;

    public NotificationCleanupJob(
        IAppDbContext context,
        ILogger<NotificationCleanupJob> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task ExecuteAsync()
    {
        _logger.LogInformation("NotificationCleanupJob started");

        var thresholdDate = DateTime.UtcNow.AddDays(-30);

        var oldNotifications = await _context.Notifications
            .Where(x =>
                x.IsRead &&
                x.CreatedDate < thresholdDate)
            .ToListAsync();

        if (!oldNotifications.Any())
        {
            _logger.LogInformation("No old notifications found");
            return;
        }

        foreach (var notification in oldNotifications)
        {
            notification.IsDeleted = true;
            notification.DeletedDate = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "NotificationCleanupJob completed. Deleted count: {Count}",
            oldNotifications.Count);
    }
}