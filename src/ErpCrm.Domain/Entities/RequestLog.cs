using ErpCrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ErpCrm.Domain.Entities;

public class RequestLog : BaseEntity
{
    public int? UserId { get; set; }

    public string Method { get; set; } = null!;

    public string Path { get; set; } = null!;

    public int StatusCode { get; set; }

    public long ExecutionTimeMs { get; set; }

    public string? IPAddress { get; set; }

    public string? UserAgent { get; set; }

    public string? CorrelationId { get; set; }
}