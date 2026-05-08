using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace ErpCrm.Application.Features.AuditLogs.Commands.DeleteAuditLog;
public class DeleteAuditLogCommandHandler : IRequestHandler<DeleteAuditLogCommand, Result<bool>>
{
    private readonly IAppDbContext _context;
    public DeleteAuditLogCommandHandler(IAppDbContext context) => _context = context;
    public async Task<Result<bool>> Handle(DeleteAuditLogCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.AuditLogs.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity is null) return Result<bool>.NotFound("AuditLog not found");
        entity.IsDeleted = true; entity.DeletedDate = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);
        return Result<bool>.Ok(true, "AuditLog deleted successfully");
    }
}
