using ErpCrm.Domain.Common;
using ErpCrm.Domain.Enums;

namespace ErpCrm.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string OrderNumber { get; set; } = null!;

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public int CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; } = null!;

        public OrderStatus Status { get; set; }

        public decimal TotalAmount { get; set; }

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        public Payment? Payment { get; set; }
    }
}
