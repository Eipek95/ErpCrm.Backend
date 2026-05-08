using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Domain.Entities;
using MediatR;
namespace ErpCrm.Application.Features.Notifications.Commands.CreateNotification;
public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, Result<int>>
{
    private readonly IAppDbContext _context;
    public CreateNotificationCommandHandler(IAppDbContext context) => _context = context;
    public async Task<Result<int>> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        var entity = new Notification { UserId = request.UserId, Title = request.Title, Message = request.Message, IsRead = request.IsRead, ReadDate = request.IsRead ? DateTime.UtcNow : null, CreatedDate = DateTime.UtcNow };
        await _context.Notifications.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Result<int>.Created(entity.Id, "Notification created successfully");
    }
}
