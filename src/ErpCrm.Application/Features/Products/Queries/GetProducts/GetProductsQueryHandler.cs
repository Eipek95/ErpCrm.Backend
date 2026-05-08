using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Products.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace ErpCrm.Application.Features.Products.Queries.GetProducts;
public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Result<PagedResult<ProductDto>>>
{
    private readonly IAppDbContext _context;
    public GetProductsQueryHandler(IAppDbContext context) => _context = context;
    public async Task<Result<PagedResult<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Products.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            query = query.Where(x => x.Name.ToLower().Contains(search) || x.SKU.ToLower().Contains(search));
        }
        if (request.CategoryId.HasValue) query = query.Where(x => x.CategoryId == request.CategoryId.Value);
        if (request.IsActive.HasValue) query = query.Where(x => x.IsActive == request.IsActive.Value);
        if (request.IsPopular.HasValue) query = query.Where(x => x.IsPopular == request.IsPopular.Value);
        query = request.SortBy?.ToLower() switch
        {
            "createddate" => request.SortDescending ? query.OrderByDescending(x => x.CreatedDate) : query.OrderBy(x => x.CreatedDate),
            _ => query.OrderByDescending(x => x.CreatedDate)
        };
        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query.Skip(request.Skip).Take(request.PageSize).Select(x => new ProductDto { Id = x.Id, Name = x.Name, SKU = x.SKU, Price = x.Price, CostPrice = x.CostPrice, IsPopular = x.IsPopular, IsActive = x.IsActive, CategoryId = x.CategoryId, CategoryName = x.Category.Name, CreatedDate = x.CreatedDate }).ToListAsync(cancellationToken);
        return Result<PagedResult<ProductDto>>.Ok(new PagedResult<ProductDto> { Items = items, Page = request.Page, PageSize = request.PageSize, TotalCount = totalCount });
    }
}
