using System;
using System.Collections.Generic;
using System.Text;

namespace ErpCrm.Application.Features.Dashboard.DTOs
{
    public class MonthlySalesDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; } = null!;
        public decimal TotalSales { get; set; }
        public int OrderCount { get; set; }
    }
}
