using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Dashboard.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.Dashboard.Queries.GetTopProducts;

public class GetTopProductsQuery : IRequest<Result<List<TopProductDto>>>
{
    public int Count { get; set; } = 10;
}