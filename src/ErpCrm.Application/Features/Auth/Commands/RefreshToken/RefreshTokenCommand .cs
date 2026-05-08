using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Auth.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<Result<AuthResponseDto>>
{
    public string RefreshToken { get; set; } = null!;
}