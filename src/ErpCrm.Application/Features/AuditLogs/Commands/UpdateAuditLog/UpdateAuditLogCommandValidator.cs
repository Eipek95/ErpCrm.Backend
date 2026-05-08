using FluentValidation;

namespace ErpCrm.Application.Features.AuditLogs.Commands.UpdateAuditLog;

public class UpdateAuditLogCommandValidator : AbstractValidator<UpdateAuditLogCommand>
{
    public UpdateAuditLogCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.UserId).GreaterThan(0).When(x => x.UserId.HasValue);
        RuleFor(x => x.Action).NotEmpty().MaximumLength(100);
        RuleFor(x => x.EntityName).NotEmpty().MaximumLength(150);
        RuleFor(x => x.IPAddress).MaximumLength(60);
    }
}
