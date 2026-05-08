using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Notifications.DTOs;
using MediatR;
namespace ErpCrm.Application.Features.Notifications.Queries.GetNotificationById;
public record GetNotificationByIdQuery(int Id) : IRequest<Result<NotificationDto>>;
