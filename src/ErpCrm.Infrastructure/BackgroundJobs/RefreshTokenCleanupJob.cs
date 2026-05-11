using ErpCrm.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ErpCrm.Infrastructure.BackgroundJobs;

public class RefreshTokenCleanupJob
{
    private readonly IAppDbContext _context;
    private readonly ILogger<RefreshTokenCleanupJob> _logger;

    public RefreshTokenCleanupJob(
        IAppDbContext context,
        ILogger<RefreshTokenCleanupJob> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task ExecuteAsync()
    {
        _logger.LogInformation("RefreshTokenCleanupJob started");

        var thresholdDate = DateTime.UtcNow.AddDays(-30);

        var oldTokens = await _context.RefreshTokens
            .Where(x =>
                (x.ExpiresAt < thresholdDate ||
                 x.RevokedAt < thresholdDate) &&
                !x.IsDeleted)
            .ToListAsync();

        if (!oldTokens.Any())
        {
            _logger.LogInformation("No old refresh tokens found");
            return;
        }

        foreach (var token in oldTokens)
        {
            token.IsDeleted = true;
            token.DeletedDate = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "RefreshTokenCleanupJob completed. Deleted count: {Count}",
            oldTokens.Count);
    }
}