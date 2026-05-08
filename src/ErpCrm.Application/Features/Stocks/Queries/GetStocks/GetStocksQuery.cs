using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Stocks.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.Stocks.Queries.GetStocks;

public class GetStocksQuery : PagedRequest, IRequest<Result<PagedResult<StockDto>>>
{
    public int? ProductId { get; set; }
    public int? WarehouseId { get; set; }
    public int? ProductVariantId { get; set; }
}
