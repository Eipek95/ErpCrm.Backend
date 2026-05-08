using ErpCrm.Domain.Common;
using ErpCrm.Domain.Enums;

namespace ErpCrm.Domain.Entities
{
    public class StockMovement : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; } = null!;

        public StockMovementType MovementType { get; set; }

        public int Quantity { get; set; }

        public string? ReferenceNumber { get; set; }
        public string? Description { get; set; }
    }
}
