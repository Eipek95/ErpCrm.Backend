using ErpCrm.Application.Common.Results;
using MediatR;
namespace ErpCrm.Application.Features.AuditLogs.Commands.DeleteAuditLog;
public record DeleteAuditLogCommand(int Id) : IRequest<Result<bool>>;
