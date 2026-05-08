using FluentValidation;

namespace ErpCrm.Application.Features.AuditLogs.Commands.CreateAuditLog;

public class CreateAuditLogCommandValidator : AbstractValidator<CreateAuditLogCommand>
{
    public CreateAuditLogCommandValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0).When(x => x.UserId.HasValue);
        RuleFor(x => x.Action).NotEmpty().MaximumLength(100);
        RuleFor(x => x.EntityName).NotEmpty().MaximumLength(150);
        RuleFor(x => x.IPAddress).MaximumLength(60);
    }
}
