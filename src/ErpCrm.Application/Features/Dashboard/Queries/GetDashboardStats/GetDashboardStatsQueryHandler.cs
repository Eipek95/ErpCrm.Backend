using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Dashboard.DTOs;
using ErpCrm.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Dashboard.Queries.GetDashboardStats;

public class GetDashboardStatsQueryHandler
    : IRequestHandler<GetDashboardStatsQuery, Result<DashboardStatsDto>>
{
    private readonly IAppDbContext _context;

    public GetDashboardStatsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<DashboardStatsDto>> Handle(
        GetDashboardStatsQuery request,
        CancellationToken cancellationToken)
    {
        var totalOrders = await _context.Orders
            .AsNoTracking()
            .CountAsync(cancellationToken);

        var totalSales = await _context.Orders
            .AsNoTracking()
            .Where(x => x.Status != OrderStatus.Cancelled)
            .SumAsync(x => x.TotalAmount, cancellationToken);

        var activeCustomers = await _context.Customers
            .AsNoTracking()
            .CountAsync(x => x.IsActive, cancellationToken);

        var totalProducts = await _context.Products
            .AsNoTracking()
            .CountAsync(cancellationToken);

        var lowStockCount = await _context.Stocks
            .AsNoTracking()
            .CountAsync(x => x.Quantity - x.ReservedQuantity <= 10, cancellationToken);

        var pendingPayments = await _context.Payments
            .AsNoTracking()
            .CountAsync(x => x.Status == PaymentStatus.Pending, cancellationToken);

        var dto = new DashboardStatsDto
        {
            TotalOrders = totalOrders,
            TotalSales = totalSales,
            ActiveCustomers = activeCustomers,
            TotalProducts = totalProducts,
            LowStockCount = lowStockCount,
            PendingPayments = pendingPayments
        };

        return Result<DashboardStatsDto>.Ok(dto);
    }
}