using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.OrderItems.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.OrderItems.Queries.GetOrderItems;

public class GetOrderItemsQueryHandler : IRequestHandler<GetOrderItemsQuery, Result<PagedResult<OrderItemDto>>>
{
    private readonly IAppDbContext _context;

    public GetOrderItemsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PagedResult<OrderItemDto>>> Handle(GetOrderItemsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.OrderItems.AsNoTracking().AsQueryable();

        if (request.OrderId.HasValue)
            query = query.Where(x => x.OrderId == request.OrderId.Value);

        if (request.ProductId.HasValue)
            query = query.Where(x => x.ProductId == request.ProductId.Value);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            query = query.Where(x => x.Product.Name.ToLower().Contains(search));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(x => x.CreatedDate)
            .Skip(request.Skip)
            .Take(request.PageSize)
            .Select(x => new OrderItemDto
            {
                Id = x.Id,
                OrderId = x.OrderId,
                ProductId = x.ProductId,
                ProductName = x.Product.Name,
                ProductVariantId = x.ProductVariantId,
                VariantCode = x.ProductVariant != null ? x.ProductVariant.VariantCode : null,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                TotalPrice = x.TotalPrice
            })
            .ToListAsync(cancellationToken);

        return Result<PagedResult<OrderItemDto>>.Ok(new PagedResult<OrderItemDto>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        });
    }
}
