using System;
using System.Collections.Generic;
using System.Text;

namespace ErpCrm.Application.Features.Dashboard.DTOs
{
    public class DashboardStatsDto
    {
        public int TotalOrders { get; set; }
        public decimal TotalSales { get; set; }
        public int ActiveCustomers { get; set; }
        public int TotalProducts { get; set; }
        public int LowStockCount { get; set; }
        public int PendingPayments { get; set; }
    }
}
