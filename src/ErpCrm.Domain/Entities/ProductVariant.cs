using ErpCrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ErpCrm.Domain.Entities
{
    public class ProductVariant : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public string VariantCode { get; set; } = null!;
        public string? Color { get; set; }
        public string? Size { get; set; }
        public decimal? Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    }
}
