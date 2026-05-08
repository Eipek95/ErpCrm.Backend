using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<bool>>
{
    private readonly IAppDbContext _context;

    public DeleteUserCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity is null)
            return Result<bool>.NotFound("User not found");

        entity.IsDeleted = true;
        entity.DeletedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Ok(true, "User deleted successfully");
    }
}
