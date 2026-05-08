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

    public GetRecentOrdersQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<RecentOrderDto>>> Handle(
        GetRecentOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var count = request.Count <= 0 ? 10 : request.Count;

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

        return Result<List<RecentOrderDto>>.Ok(data);
    }
}