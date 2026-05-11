using ErpCrm.Application.Common.Models;
using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.Auth.Logout;

public class LogoutCommand : IRequest<Result<bool>>
{
    public string RefreshToken { get; set; } = null!;
}