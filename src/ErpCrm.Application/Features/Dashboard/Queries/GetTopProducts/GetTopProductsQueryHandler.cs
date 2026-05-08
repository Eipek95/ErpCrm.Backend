using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Dashboard.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Dashboard.Queries.GetTopProducts;

public class GetTopProductsQueryHandler
    : IRequestHandler<GetTopProductsQuery, Result<List<TopProductDto>>>
{
    private readonly IAppDbContext _context;

    public GetTopProductsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<TopProductDto>>> Handle(
        GetTopProductsQuery request,
        CancellationToken cancellationToken)
    {
        var count = request.Count <= 0 ? 10 : request.Count;

        var data = await _context.OrderItems
            .AsNoTracking()
            .GroupBy(x => new
            {
                x.ProductId,
                x.Product.Name
            })
            .Select(x => new TopProductDto
            {
                ProductId = x.Key.ProductId,
                ProductName = x.Key.Name,
                TotalQuantitySold = x.Sum(i => i.Quantity),
                TotalSales = x.Sum(i => i.TotalPrice)
            })
            .OrderByDescending(x => x.TotalQuantitySold)
            .Take(count)
            .ToListAsync(cancellationToken);

        return Result<List<TopProductDto>>.Ok(data);
    }
}