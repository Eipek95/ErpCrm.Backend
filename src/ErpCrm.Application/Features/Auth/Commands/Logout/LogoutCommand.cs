using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.Auth.Commands.Logout;

public class LogoutCommand : IRequest<Result<bool>>
{
}