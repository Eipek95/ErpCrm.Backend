using FluentValidation;

namespace ErpCrm.Application.Features.Notifications.Commands.UpdateNotification;

public class UpdateNotificationCommandValidator : AbstractValidator<UpdateNotificationCommand>
{
    public UpdateNotificationCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Message).NotEmpty().MaximumLength(1000);
    }
}
