using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.AuditLogs.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace ErpCrm.Application.Features.AuditLogs.Queries.GetAuditLogById;

public class GetAuditLogByIdQueryHandler : IRequestHandler<GetAuditLogByIdQuery, Result<AuditLogDto>>
{
    private readonly IAppDbContext _context;
    public GetAuditLogByIdQueryHandler(IAppDbContext context) => _context = context;
    public async Task<Result<AuditLogDto>> Handle(GetAuditLogByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _context.AuditLogs.AsNoTracking().Where(x => x.Id == request.Id).Select(x => new AuditLogDto { Id = x.Id, UserId = x.UserId, Action = x.Action, EntityName = x.EntityName, OldValues = x.OldValues, NewValues = x.NewValues, IPAddress = x.IPAddress, CreatedDate = x.CreatedDate }).FirstOrDefaultAsync(cancellationToken);
        return item is null ? Result<AuditLogDto>.NotFound("AuditLog not found") : Result<AuditLogDto>.Ok(item);
    }
}
