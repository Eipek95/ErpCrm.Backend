using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Orders.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Orders.Queries.GetOrders;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, Result<PagedResult<OrderDto>>>
{
    private readonly IAppDbContext _context;

    public GetOrdersQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PagedResult<OrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Orders.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            query = query.Where(x =>
                x.OrderNumber.ToLower().Contains(search) ||
                x.Customer.CompanyName.ToLower().Contains(search));
        }

        if (request.CustomerId.HasValue)
            query = query.Where(x => x.CustomerId == request.CustomerId.Value);

        if (request.CreatedByUserId.HasValue)
            query = query.Where(x => x.CreatedByUserId == request.CreatedByUserId.Value);

        if (request.Status.HasValue)
            query = query.Where(x => (int)x.Status == request.Status.Value);

        query = request.SortBy?.ToLower() switch
        {
            "totalamount" => request.SortDescending ? query.OrderByDescending(x => x.TotalAmount) : query.OrderBy(x => x.TotalAmount),
            "createddate" => request.SortDescending ? query.OrderByDescending(x => x.CreatedDate) : query.OrderBy(x => x.CreatedDate),
            _ => query.OrderByDescending(x => x.CreatedDate)
        };

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip(request.Skip)
            .Take(request.PageSize)
            .Select(x => new OrderDto
            {
                Id = x.Id,
                OrderNumber = x.OrderNumber,
                CustomerId = x.CustomerId,
                CustomerName = x.Customer.CompanyName,
                CreatedByUserId = x.CreatedByUserId,
                CreatedByUserName = x.CreatedByUser.FullName,
                Status = (int)x.Status,
                TotalAmount = x.TotalAmount,
                CreatedDate = x.CreatedDate
            })
            .ToListAsync(cancellationToken);

        return Result<PagedResult<OrderDto>>.Ok(new PagedResult<OrderDto>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        });
    }
}
