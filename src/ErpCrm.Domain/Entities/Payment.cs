using ErpCrm.Domain.Common;
using ErpCrm.Domain.Enums;

namespace ErpCrm.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public PaymentStatus Status { get; set; }
        public PaymentMethod Method { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaidDate { get; set; }
    }
}
