using ErpCrm.Application.Common.Results;
using MediatR;
namespace ErpCrm.Application.Features.Notifications.Commands.UpdateNotification;
public class UpdateNotificationCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = null!;
    public string Message { get; set; } = null!;
    public bool IsRead { get; set; }
}
