using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.OrderItems.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.OrderItems.Queries.GetOrderItems;

public class GetOrderItemsQuery : PagedRequest, IRequest<Result<PagedResult<OrderItemDto>>>
{
    public int? OrderId { get; set; }
    public int? ProductId { get; set; }
}
