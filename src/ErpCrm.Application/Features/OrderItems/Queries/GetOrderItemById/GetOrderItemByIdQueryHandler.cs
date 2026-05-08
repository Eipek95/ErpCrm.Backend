using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.OrderItems.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.OrderItems.Queries.GetOrderItemById;

public class GetOrderItemByIdQueryHandler : IRequestHandler<GetOrderItemByIdQuery, Result<OrderItemDto>>
{
    private readonly IAppDbContext _context;

    public GetOrderItemByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<OrderItemDto>> Handle(GetOrderItemByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _context.OrderItems
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
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
            .FirstOrDefaultAsync(cancellationToken);

        return item is null
            ? Result<OrderItemDto>.NotFound("Order item not found")
            : Result<OrderItemDto>.Ok(item);
    }
}
