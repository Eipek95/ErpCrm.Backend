using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Domain.Entities;
using MediatR;
namespace ErpCrm.Application.Features.AuditLogs.Commands.CreateAuditLog;
public class CreateAuditLogCommandHandler : IRequestHandler<CreateAuditLogCommand, Result<int>>
{
    private readonly IAppDbContext _context;
    public CreateAuditLogCommandHandler(IAppDbContext context) => _context = context;
    public async Task<Result<int>> Handle(CreateAuditLogCommand request, CancellationToken cancellationToken)
    {
        var entity = new AuditLog { UserId = request.UserId, Action = request.Action, EntityName = request.EntityName, OldValues = request.OldValues, NewValues = request.NewValues, IPAddress = request.IPAddress, CreatedDate = DateTime.UtcNow };
        await _context.AuditLogs.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Result<int>.Created(entity.Id, "AuditLog created successfully");
    }
}
