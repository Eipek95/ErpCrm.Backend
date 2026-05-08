using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Payments.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Payments.Queries.GetPayments;

public class GetPaymentsQueryHandler : IRequestHandler<GetPaymentsQuery, Result<PagedResult<PaymentDto>>>
{
    private readonly IAppDbContext _context;

    public GetPaymentsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PagedResult<PaymentDto>>> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Payments.AsNoTracking().AsQueryable();

        if (request.OrderId.HasValue)
            query = query.Where(x => x.OrderId == request.OrderId.Value);

        if (request.Status.HasValue)
            query = query.Where(x => (int)x.Status == request.Status.Value);

        if (request.Method.HasValue)
            query = query.Where(x => (int)x.Method == request.Method.Value);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            query = query.Where(x => x.Order.OrderNumber.ToLower().Contains(search));
        }

        query = request.SortBy?.ToLower() switch
        {
            "amount" => request.SortDescending ? query.OrderByDescending(x => x.Amount) : query.OrderBy(x => x.Amount),
            "paiddate" => request.SortDescending ? query.OrderByDescending(x => x.PaidDate) : query.OrderBy(x => x.PaidDate),
            _ => query.OrderByDescending(x => x.CreatedDate)
        };

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip(request.Skip)
            .Take(request.PageSize)
            .Select(x => new PaymentDto
            {
                Id = x.Id,
                OrderId = x.OrderId,
                OrderNumber = x.Order.OrderNumber,
                Status = (int)x.Status,
                Method = (int)x.Method,
                Amount = x.Amount,
                PaidDate = x.PaidDate,
                CreatedDate = x.CreatedDate
            })
            .ToListAsync(cancellationToken);

        return Result<PagedResult<PaymentDto>>.Ok(new PagedResult<PaymentDto>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        });
    }
}
