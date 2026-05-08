using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.StockMovements.Commands.UpdateStockMovement;

public class UpdateStockMovementCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int WarehouseId { get; set; }
    public int MovementType { get; set; }
    public int Quantity { get; set; }
    public string? ReferenceNumber { get; set; }
    public string? Description { get; set; }
}
