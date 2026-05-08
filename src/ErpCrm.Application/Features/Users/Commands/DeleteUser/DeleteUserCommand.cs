using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.Users.Commands.DeleteUser;

public record DeleteUserCommand(int Id) : IRequest<Result<bool>>;
