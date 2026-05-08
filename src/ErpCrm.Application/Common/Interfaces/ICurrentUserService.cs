using System;
using System.Collections.Generic;
using System.Text;

namespace ErpCrm.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
        string? Email { get; }
        string? FullName { get; }
        IReadOnlyList<string> Roles { get; }
        bool IsAuthenticated { get; }
    }
}
