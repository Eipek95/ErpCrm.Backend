using ErpCrm.Application.Common.Results;
using MediatR;
namespace ErpCrm.Application.Features.AuditLogs.Commands.UpdateAuditLog;
public class UpdateAuditLogCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public string Action { get; set; } = null!;
    public string EntityName { get; set; } = null!;
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? IPAddress { get; set; }
}
