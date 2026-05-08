using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.Stocks.Commands.UpdateStock;

public class UpdateStockCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int ReservedQuantity { get; set; }
}
