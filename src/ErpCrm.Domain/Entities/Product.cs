using ErpCrm.Domain.Common;

namespace ErpCrm.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string SKU { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal CostPrice { get; set; }

        public bool IsPopular { get; set; }
        public bool IsActive { get; set; } = true;

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public ICollection<Stock> Stocks { get; set; } = new List<Stock>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
    }
}
