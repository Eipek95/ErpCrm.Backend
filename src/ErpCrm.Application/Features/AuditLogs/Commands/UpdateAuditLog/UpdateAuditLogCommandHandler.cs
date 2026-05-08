using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace ErpCrm.Application.Features.AuditLogs.Commands.UpdateAuditLog;
public class UpdateAuditLogCommandHandler : IRequestHandler<UpdateAuditLogCommand, Result<bool>>
{
    private readonly IAppDbContext _context;
    public UpdateAuditLogCommandHandler(IAppDbContext context) => _context = context;
    public async Task<Result<bool>> Handle(UpdateAuditLogCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.AuditLogs.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity is null) return Result<bool>.NotFound("AuditLog not found");
        entity.Action = request.Action; entity.EntityName = request.EntityName; entity.OldValues = request.OldValues; entity.NewValues = request.NewValues; entity.IPAddress = request.IPAddress;
        entity.UpdatedDate = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);
        return Result<bool>.Ok(true, "AuditLog updated successfully");
    }
}
