using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Dashboard.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Dashboard.Queries.GetRecentOrders;

public class GetRecentOrdersQueryHandler
    : IRequestHandler<GetRecentOrdersQuery, Result<List<RecentOrderDto>>>
{
    private readonly IAppDbContext _context;
    private readonly ICacheService _cacheService;
    public GetRecentOrdersQueryHandler(IAppDbContext context, ICacheService cacheService)
    {
        _context = context;
        _cacheService = cacheService;
    }

    public async Task<Result<List<RecentOrderDto>>> Handle(
    GetRecentOrdersQuery request,
    CancellationToken cancellationToken)
    {
        var count = request.Count <= 0 ? 10 : request.Count;

        var cacheKey = $"dashboard:recent-orders:{count}";

        var cached = await _cacheService
            .GetAsync<List<RecentOrderDto>>(cacheKey);

        if (cached is not null)
            return Result<List<RecentOrderDto>>.Ok(cached);

        var data = await _context.Orders
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedDate)
            .Take(count)
            .Select(x => new RecentOrderDto
            {
                OrderId = x.Id,
                OrderNumber = x.OrderNumber,
                CustomerName = x.Customer.CompanyName,
                CreatedByUserName = x.CreatedByUser.FullName,
                TotalAmount = x.TotalAmount,
                Status = (int)x.Status,
                CreatedDate = x.CreatedDate
            })
            .ToListAsync(cancellationToken);

        await _cacheService.SetAsync(
            cacheKey,
            data,
            TimeSpan.FromMinutes(2));

        return Result<List<RecentOrderDto>>.Ok(data);
    }
}