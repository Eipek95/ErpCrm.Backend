using System;
using System.Collections.Generic;
using System.Text;

namespace ErpCrm.Application.Common.Interfaces
{
    public interface IFakeDataSeeder
    {
        Task SeedAsync(CancellationToken cancellationToken = default);
    }
}
