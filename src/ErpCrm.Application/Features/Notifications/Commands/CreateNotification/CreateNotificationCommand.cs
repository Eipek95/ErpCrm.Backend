using ErpCrm.Application.Common.Results;
using MediatR;
namespace ErpCrm.Application.Features.Notifications.Commands.CreateNotification;
public class CreateNotificationCommand : IRequest<Result<int>>
{
    public int UserId { get; set; }
    public string Title { get; set; } = null!;
    public string Message { get; set; } = null!;
    public bool IsRead { get; set; }
}
