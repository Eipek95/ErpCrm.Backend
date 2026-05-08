using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Notifications.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace ErpCrm.Application.Features.Notifications.Queries.GetNotificationById;
public class GetNotificationByIdQueryHandler : IRequestHandler<GetNotificationByIdQuery, Result<NotificationDto>>
{
    private readonly IAppDbContext _context;
    public GetNotificationByIdQueryHandler(IAppDbContext context) => _context = context;
    public async Task<Result<NotificationDto>> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _context.Notifications.AsNoTracking().Where(x => x.Id == request.Id).Select(x => new NotificationDto { Id = x.Id, UserId = x.UserId, Title = x.Title, Message = x.Message, IsRead = x.IsRead, ReadDate = x.ReadDate, CreatedDate = x.CreatedDate }).FirstOrDefaultAsync(cancellationToken);
        return item is null ? Result<NotificationDto>.NotFound("Notification not found") : Result<NotificationDto>.Ok(item);
    }
}
