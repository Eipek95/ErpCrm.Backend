using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Dashboard.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.Dashboard.Queries.GetWarehouseStock;

public class GetWarehouseStockQuery : IRequest<Result<List<WarehouseStockDto>>>
{
}