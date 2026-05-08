using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Roles.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Roles.Queries.GetRoleById;

public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Result<RoleDto>>
{
    private readonly IAppDbContext _context;

    public GetRoleByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<RoleDto>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _context.Roles
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new RoleDto
            {
                Id = x.Id,
                Name = x.Name,
                CreatedDate = x.CreatedDate
            })
            .FirstOrDefaultAsync(cancellationToken);

        return item is null
            ? Result<RoleDto>.NotFound("Role not found")
            : Result<RoleDto>.Ok(item);
    }
}
