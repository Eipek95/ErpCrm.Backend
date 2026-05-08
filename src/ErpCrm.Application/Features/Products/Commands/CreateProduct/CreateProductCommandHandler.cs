using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Domain.Entities;
using MediatR;
namespace ErpCrm.Application.Features.Products.Commands.CreateProduct;
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<int>>
{
    private readonly IAppDbContext _context;
    public CreateProductCommandHandler(IAppDbContext context) => _context = context;
    public async Task<Result<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = new Product { Name = request.Name, SKU = request.SKU, Price = request.Price, CostPrice = request.CostPrice, IsPopular = request.IsPopular, IsActive = request.IsActive, CategoryId = request.CategoryId, CreatedDate = DateTime.UtcNow };
        await _context.Products.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Result<int>.Created(entity.Id, "Product created successfully");
    }
}
