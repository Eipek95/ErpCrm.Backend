using FluentValidation;

namespace ErpCrm.Application.Features.Payments.Commands.UpdatePayment;

public class UpdatePaymentCommandValidator : AbstractValidator<UpdatePaymentCommand>
{
    public UpdatePaymentCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Status).InclusiveBetween(1, 4);
        RuleFor(x => x.Method).InclusiveBetween(1, 3);
        RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
        RuleFor(x => x.PaidDate).NotEmpty();
    }
}
