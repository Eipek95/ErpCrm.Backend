using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Users.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.Users.Queries.GetUserById;

public record GetUserByIdQuery(int Id) : IRequest<Result<UserDto>>;
