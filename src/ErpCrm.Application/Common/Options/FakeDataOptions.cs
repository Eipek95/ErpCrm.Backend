using System;
using System.Collections.Generic;
using System.Text;

namespace ErpCrm.Application.Common.Options
{
    public class FakeDataOptions
    {
        public int UserCount { get; set; } = 10000;
        public int CustomerCount { get; set; } = 5000;
        public int ProductCount { get; set; } = 5000;
        public int OrderCount { get; set; } = 100000;
        public int BatchSize { get; set; } = 1000;
    }
}
