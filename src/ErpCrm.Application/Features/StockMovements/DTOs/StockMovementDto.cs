namespace ErpCrm.Application.Features.StockMovements.DTOs;

public class StockMovementDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int WarehouseId { get; set; }
    public int MovementType { get; set; }
    public int Quantity { get; set; }
    public string? ReferenceNumber { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
}
