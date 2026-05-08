using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.Stocks.Commands.CreateStock;

public class CreateStockCommand : IRequest<Result<int>>
{
    public int ProductId { get; set; }
    public int? ProductVariantId { get; set; }
    public int WarehouseId { get; set; }
    public int Quantity { get; set; }
    public int ReservedQuantity { get; set; }
}
