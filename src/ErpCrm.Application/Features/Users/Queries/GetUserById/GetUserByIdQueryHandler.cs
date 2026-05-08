using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Users.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
    private readonly IAppDbContext _context;

    public GetUserByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _context.Users
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new UserDto
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                IsActive = x.IsActive,
                CreatedDate = x.CreatedDate
            })
            .FirstOrDefaultAsync(cancellationToken);

        return item is null
            ? Result<UserDto>.NotFound("User not found")
            : Result<UserDto>.Ok(item);
    }
}
