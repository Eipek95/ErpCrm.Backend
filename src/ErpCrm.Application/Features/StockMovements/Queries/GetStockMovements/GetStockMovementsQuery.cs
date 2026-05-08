using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.StockMovements.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.StockMovements.Queries.GetStockMovements;

public class GetStockMovementsQuery : PagedRequest, IRequest<Result<PagedResult<StockMovementDto>>>
{
}
