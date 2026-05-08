using ErpCrm.Domain.Common;

namespace ErpCrm.Domain.Entities
{
    public class Stock : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int? ProductVariantId { get; set; }
        public ProductVariant? ProductVariant { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; } = null!;
        public int Quantity { get; set; }
        public int ReservedQuantity { get; set; }
    }
}
