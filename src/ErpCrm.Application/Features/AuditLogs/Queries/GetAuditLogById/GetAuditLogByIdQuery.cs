using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.AuditLogs.DTOs;
using MediatR;
namespace ErpCrm.Application.Features.AuditLogs.Queries.GetAuditLogById;
public record GetAuditLogByIdQuery(int Id) : IRequest<Result<AuditLogDto>>;
