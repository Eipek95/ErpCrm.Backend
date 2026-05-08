using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Orders.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.Orders.Queries.GetOrders;

public class GetOrdersQuery : PagedRequest, IRequest<Result<PagedResult<OrderDto>>>
{
    public int? CustomerId { get; set; }
    public int? CreatedByUserId { get; set; }
    public int? Status { get; set; }
}
