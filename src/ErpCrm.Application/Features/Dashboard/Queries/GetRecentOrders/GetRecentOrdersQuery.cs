using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Dashboard.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.Dashboard.Queries.GetRecentOrders;

public class GetRecentOrdersQuery : IRequest<Result<List<RecentOrderDto>>>
{
    public int Count { get; set; } = 10;
}