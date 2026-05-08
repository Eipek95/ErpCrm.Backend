using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Domain.Entities;
using MediatR;
namespace ErpCrm.Application.Features.Categories.Commands.CreateCategory;
public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<int>>
{
    private readonly IAppDbContext _context;
    public CreateCategoryCommandHandler(IAppDbContext context) => _context = context;
    public async Task<Result<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = new Category { Name = request.Name, Description = request.Description, CreatedDate = DateTime.UtcNow };
        await _context.Categories.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Result<int>.Created(entity.Id, "Category created successfully");
    }
}
