using ErpCrm.Application.Common.Results;
using MediatR;
namespace ErpCrm.Application.Features.AuditLogs.Commands.CreateAuditLog;
public class CreateAuditLogCommand : IRequest<Result<int>>
{
    public int? UserId { get; set; }
    public string Action { get; set; } = null!;
    public string EntityName { get; set; } = null!;
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? IPAddress { get; set; }
}
