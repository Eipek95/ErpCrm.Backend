using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Warehouses.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace ErpCrm.Application.Features.Warehouses.Queries.GetWarehouses;
public class GetWarehousesQueryHandler : IRequestHandler<GetWarehousesQuery, Result<PagedResult<WarehouseDto>>>
{
    private readonly IAppDbContext _context;
    public GetWarehousesQueryHandler(IAppDbContext context) => _context = context;
    public async Task<Result<PagedResult<WarehouseDto>>> Handle(GetWarehousesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Warehouses.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            query = query.Where(x => x.Name.ToLower().Contains(search) || x.City.ToLower().Contains(search) || (x.Address != null && x.Address.ToLower().Contains(search)));
        }
        if (request.IsActive.HasValue) query = query.Where(x => x.IsActive == request.IsActive.Value);
        if (!string.IsNullOrWhiteSpace(request.City)) query = query.Where(x => x.City == request.City);
        query = request.SortBy?.ToLower() switch
        {
            "createddate" => request.SortDescending ? query.OrderByDescending(x => x.CreatedDate) : query.OrderBy(x => x.CreatedDate),
            _ => query.OrderByDescending(x => x.CreatedDate)
        };
        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query.Skip(request.Skip).Take(request.PageSize).Select(x => new WarehouseDto { Id = x.Id, Name = x.Name, City = x.City, Address = x.Address, IsActive = x.IsActive, CreatedDate = x.CreatedDate }).ToListAsync(cancellationToken);
        return Result<PagedResult<WarehouseDto>>.Ok(new PagedResult<WarehouseDto> { Items = items, Page = request.Page, PageSize = request.PageSize, TotalCount = totalCount });
    }
}
