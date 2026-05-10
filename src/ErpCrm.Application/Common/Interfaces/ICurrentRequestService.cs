namespace ErpCrm.Application.Common.Interfaces;

public interface ICurrentRequestService
{
    string? GetIpAddress();
    string? GetUserAgent();
    string? GetEndpoint();
    string? GetHttpMethod();
    string? GetCorrelationId();
    long? GetExecutionTimeMs();
}