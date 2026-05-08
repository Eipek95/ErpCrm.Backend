using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace ErpCrm.Application.Features.Notifications.Commands.UpdateNotification;
public class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationCommand, Result<bool>>
{
    private readonly IAppDbContext _context;
    public UpdateNotificationCommandHandler(IAppDbContext context) => _context = context;
    public async Task<Result<bool>> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Notifications.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity is null) return Result<bool>.NotFound("Notification not found");
        entity.Title = request.Title; entity.Message = request.Message; entity.IsRead = request.IsRead; entity.ReadDate = request.IsRead ? DateTime.UtcNow : null;
        entity.UpdatedDate = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);
        return Result<bool>.Ok(true, "Notification updated successfully");
    }
}
