using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Customers.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Customers.Queries.GetCustomers;

public class GetCustomersQueryHandler
    : IRequestHandler<GetCustomersQuery, Result<PagedResult<CustomerDto>>>
{
    private readonly IAppDbContext _context;

    public GetCustomersQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PagedResult<CustomerDto>>> Handle(
        GetCustomersQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Customers
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();

            query = query.Where(x =>
                x.CompanyName.ToLower().Contains(search) ||
                x.ContactName!.ToLower().Contains(search) ||
                x.Email!.ToLower().Contains(search));
        }

        if (request.IsActive.HasValue)
            query = query.Where(x => x.IsActive == request.IsActive.Value);

        if (!string.IsNullOrWhiteSpace(request.City))
            query = query.Where(x => x.City == request.City);

        query = request.SortBy?.ToLower() switch
        {
            "companyname" => request.SortDescending
                ? query.OrderByDescending(x => x.CompanyName)
                : query.OrderBy(x => x.CompanyName),

            "city" => request.SortDescending
                ? query.OrderByDescending(x => x.City)
                : query.OrderBy(x => x.City),

            "createddate" => request.SortDescending
                ? query.OrderByDescending(x => x.CreatedDate)
                : query.OrderBy(x => x.CreatedDate),

            _ => query.OrderByDescending(x => x.CreatedDate)
        };

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip(request.Skip)
            .Take(request.PageSize)
            .Select(x => new CustomerDto
            {
                Id = x.Id,
                CompanyName = x.CompanyName,
                ContactName = x.ContactName,
                Email = x.Email,
                Phone = x.Phone,
                City = x.City,
                District = x.District,
                IsActive = x.IsActive,
                CreatedDate = x.CreatedDate
            })
            .ToListAsync(cancellationToken);

        var result = new PagedResult<CustomerDto>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        };

        return Result<PagedResult<CustomerDto>>.Ok(result);
    }
}