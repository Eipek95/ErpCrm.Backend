using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Domain.Entities;
using MediatR;

namespace ErpCrm.Application.Features.Roles.Commands.CreateRole;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result<int>>
{
    private readonly IAppDbContext _context;

    public CreateRoleCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = new Role
        {
            Name = request.Name,
            CreatedDate = DateTime.UtcNow
        };

        await _context.Roles.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<int>.Created(entity.Id, "Role created successfully");
    }
}
