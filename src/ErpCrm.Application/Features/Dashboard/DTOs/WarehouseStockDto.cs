using System;
using System.Collections.Generic;
using System.Text;

namespace ErpCrm.Application.Features.Dashboard.DTOs
{
    public class WarehouseStockDto
    {
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; } = null!;
        public int TotalQuantity { get; set; }
        public int ReservedQuantity { get; set; }
        public int AvailableQuantity { get; set; }
        public int ProductCount { get; set; }
    }
}
