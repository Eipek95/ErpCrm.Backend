using System;
using System.Collections.Generic;
using System.Text;

namespace ErpCrm.Domain.Enums
{
    public enum OrderStatus
    {
        Draft = 1,
        Confirmed = 2,
        Shipped = 3,
        Completed = 4,
        Cancelled = 5
    }
}
