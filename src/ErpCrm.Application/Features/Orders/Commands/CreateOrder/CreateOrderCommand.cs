using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<Result<int>>
{
    public int CustomerId { get; set; }
    public int CreatedByUserId { get; set; }
    public int WarehouseId { get; set; }
    public List<CreateOrderItemRequest> Items { get; set; } = new();
}

public class CreateOrderItemRequest
{
    public int ProductId { get; set; }
    public int? ProductVariantId { get; set; }
    public int Quantity { get; set; }
}
