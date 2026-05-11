using System;
using System.Collections.Generic;
using System.Text;

namespace ErpCrm.Application.Features.Orders.Messages
{
    public class OrderCreatedMessage
    {
        public string OrderNumber { get; set; } = null!;
        public int CustomerId { get; set; }
        public int CreatedByUserId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
