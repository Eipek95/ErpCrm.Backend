using System;
using System.Collections.Generic;
using System.Text;

namespace ErpCrm.Application.Features.Dashboard.DTOs
{
    public class RecentOrderDto
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string CreatedByUserName { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
