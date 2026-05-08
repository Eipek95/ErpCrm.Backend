using ErpCrm.Application.Common.Results;
using MediatR;
namespace ErpCrm.Application.Features.Notifications.Commands.DeleteNotification;
public record DeleteNotificationCommand(int Id) : IRequest<Result<bool>>;
