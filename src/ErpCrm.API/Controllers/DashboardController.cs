using ErpCrm.Application.Features.Dashboard.Queries.GetDashboardStats;
using ErpCrm.Application.Features.Dashboard.Queries.GetMonthlySales;
using ErpCrm.Application.Features.Dashboard.Queries.GetRecentOrders;
using ErpCrm.Application.Features.Dashboard.Queries.GetTopProducts;
using ErpCrm.Application.Features.Dashboard.Queries.GetWarehouseStock;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpCrm.API.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class DashboardController : ControllerBase
{
    private readonly IMediator _mediator;

    public DashboardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
    {
        var result = await _mediator.Send(new GetDashboardStatsQuery());
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("monthly-sales")]
    public async Task<IActionResult> GetMonthlySales([FromQuery] int? year)
    {
        var result = await _mediator.Send(new GetMonthlySalesQuery
        {
            Year = year
        });

        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("top-products")]
    public async Task<IActionResult> GetTopProducts([FromQuery] int count = 10)
    {
        var result = await _mediator.Send(new GetTopProductsQuery
        {
            Count = count
        });

        return StatusCode(result.StatusCode, result);
    }
    [HttpGet("warehouse-stock")]
    public async Task<IActionResult> GetWarehouseStock()
    {
        var result = await _mediator.Send(new GetWarehouseStockQuery());
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("recent-orders")]
    public async Task<IActionResult> GetRecentOrders([FromQuery] int count = 10)
    {
        var result = await _mediator.Send(new GetRecentOrdersQuery
        {
            Count = count
        });

        return StatusCode(result.StatusCode, result);
    }
}