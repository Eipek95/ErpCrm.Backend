using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.AuditLogs.DTOs;
using MediatR;
namespace ErpCrm.Application.Features.AuditLogs.Queries.GetAuditLogs;
public class GetAuditLogsQuery : PagedRequest, IRequest<Result<PagedResult<AuditLogDto>>>
{
    public int? UserId { get; set; }
    public string? Action { get; set; }
    public string? EntityName { get; set; }
}
