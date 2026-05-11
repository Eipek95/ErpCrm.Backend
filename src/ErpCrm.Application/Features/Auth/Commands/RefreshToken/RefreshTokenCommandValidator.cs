using ErpCrm.Application.Features.Auth.Commands.RefreshToken;
using FluentValidation;

namespace ErpCrm.Application.Features.Auth.RefreshToken;

public class RefreshTokenCommandValidator
    : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage("Refresh token is required.");
    }
}