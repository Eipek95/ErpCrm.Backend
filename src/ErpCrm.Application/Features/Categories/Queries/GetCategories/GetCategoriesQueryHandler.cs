using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Categories.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace ErpCrm.Application.Features.Categories.Queries.GetCategories;
public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, Result<PagedResult<CategoryDto>>>
{
    private readonly IAppDbContext _context;
    public GetCategoriesQueryHandler(IAppDbContext context) => _context = context;
    public async Task<Result<PagedResult<CategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Categories.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            query = query.Where(x => x.Name.ToLower().Contains(search) || (x.Description != null && x.Description.ToLower().Contains(search)));
        }

        query = request.SortBy?.ToLower() switch
        {
            "createddate" => request.SortDescending ? query.OrderByDescending(x => x.CreatedDate) : query.OrderBy(x => x.CreatedDate),
            _ => query.OrderByDescending(x => x.CreatedDate)
        };
        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query.Skip(request.Skip).Take(request.PageSize).Select(x => new CategoryDto { Id = x.Id, Name = x.Name, Description = x.Description, CreatedDate = x.CreatedDate }).ToListAsync(cancellationToken);
        return Result<PagedResult<CategoryDto>>.Ok(new PagedResult<CategoryDto> { Items = items, Page = request.Page, PageSize = request.PageSize, TotalCount = totalCount });
    }
}
