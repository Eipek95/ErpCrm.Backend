using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Notifications.DTOs;
using MediatR;
namespace ErpCrm.Application.Features.Notifications.Queries.GetNotifications;
public class GetNotificationsQuery : PagedRequest, IRequest<Result<PagedResult<NotificationDto>>>
{
    public int? UserId { get; set; }
    public bool? IsRead { get; set; }
}
