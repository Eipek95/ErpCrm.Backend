using FluentValidation;

namespace ErpCrm.Application.Features.Payments.Commands.CreatePayment;

public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentCommandValidator()
    {
        RuleFor(x => x.OrderId).GreaterThan(0);
        RuleFor(x => x.Status).InclusiveBetween(1, 4);
        RuleFor(x => x.Method).InclusiveBetween(1, 3);
        RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
        RuleFor(x => x.PaidDate).NotEmpty();
    }
}
