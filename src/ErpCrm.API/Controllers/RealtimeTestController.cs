using ErpCrm.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ErpCrm.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RealtimeTestController : ControllerBase
{
    private readonly IRealtimeNotificationService _realtimeNotificationService;

    public RealtimeTestController(
        IRealtimeNotificationService realtimeNotificationService)
    {
        _realtimeNotificationService = realtimeNotificationService;
    }

    [HttpPost("notification")]
    public async Task<IActionResult> SendNotification()
    {
        await _realtimeNotificationService.SendNotificationAsync(
            "Test Notification",
            "This is a realtime SignalR test notification.");

        return Ok("Realtime notification sent.");
    }

    [HttpPost("low-stock")]
    public async Task<IActionResult> SendLowStockAlert()
    {
        await _realtimeNotificationService.SendLowStockAlertAsync(
            "Test Product",
            5);

        return Ok("Realtime low stock alert sent.");
    }

    [HttpPost("order-created")]
    public async Task<IActionResult> SendOrderCreated()
    {
        await _realtimeNotificationService.SendOrderCreatedAsync(
            "ORD-TEST-001",
            12500);

        return Ok("Realtime order created event sent.");
    }
}