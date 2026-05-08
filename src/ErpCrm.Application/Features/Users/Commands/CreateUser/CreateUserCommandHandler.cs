using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Domain.Entities;
using MediatR;

namespace ErpCrm.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<int>>
{
    private readonly IAppDbContext _context;

    public CreateUserCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = new User
        {
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = request.PasswordHash,
            IsActive = request.IsActive,
            CreatedDate = DateTime.UtcNow
        };

        await _context.Users.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<int>.Created(entity.Id, "User created successfully");
    }
}
