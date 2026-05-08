using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Orders.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Result<OrderDto>>
{
    private readonly IAppDbContext _context;

    public GetOrderByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
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
                CreatedDate = x.CreatedDate,
                Items = x.Items.Select(i => new OrderItemDto
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    ProductVariantId = i.ProductVariantId,
                    VariantCode = i.ProductVariant != null ? i.ProductVariant.VariantCode : null,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.TotalPrice
                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        return order is null
            ? Result<OrderDto>.NotFound("Order not found")
            : Result<OrderDto>.Ok(order);
    }
}
