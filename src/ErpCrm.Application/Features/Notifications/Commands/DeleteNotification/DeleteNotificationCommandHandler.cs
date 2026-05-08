using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace ErpCrm.Application.Features.Notifications.Commands.DeleteNotification;
public class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand, Result<bool>>
{
    private readonly IAppDbContext _context;
    public DeleteNotificationCommandHandler(IAppDbContext context) => _context = context;
    public async Task<Result<bool>> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Notifications.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity is null) return Result<bool>.NotFound("Notification not found");
        entity.IsDeleted = true; entity.DeletedDate = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);
        return Result<bool>.Ok(true, "Notification deleted successfully");
    }
}
