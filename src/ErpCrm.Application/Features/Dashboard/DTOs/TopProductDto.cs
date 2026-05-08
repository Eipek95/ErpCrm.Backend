using System;
using System.Collections.Generic;
using System.Text;

namespace ErpCrm.Application.Features.Dashboard.DTOs
{
    public class TopProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int TotalQuantitySold { get; set; }
        public decimal TotalSales { get; set; }
    }
}
