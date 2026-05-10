using ErpCrm.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ErpCrm.Infrastructure.Services;

public class CurrentRequestService : ICurrentRequestService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentRequestService(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetIpAddress()
    {
        return _httpContextAccessor
            .HttpContext?
            .Connection?
            .RemoteIpAddress?
            .ToString();
    }

    public string? GetUserAgent()
    {
        return _httpContextAccessor
            .HttpContext?
            .Request
            .Headers["User-Agent"]
            .ToString();
    }

    public string? GetEndpoint()
    {
        return _httpContextAccessor
            .HttpContext?
            .Request
            .Path
            .ToString();
    }

    public string? GetHttpMethod()
    {
        return _httpContextAccessor
            .HttpContext?
            .Request
            .Method;
    }

    public string? GetCorrelationId()
    {
        return _httpContextAccessor
            .HttpContext?
            .TraceIdentifier;
    }

    public long? GetExecutionTimeMs()
    {
        var value = _httpContextAccessor
            .HttpContext?
            .Items["ExecutionTimeMs"];

        return value is long ms ? ms : null;
    }
}