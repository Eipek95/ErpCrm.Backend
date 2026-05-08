using ErpCrm.Domain.Common;

namespace ErpCrm.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int? ProductVariantId { get; set; }
        public ProductVariant? ProductVariant { get; set; }
    }
}
