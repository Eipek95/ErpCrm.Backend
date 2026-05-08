using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Categories.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace ErpCrm.Application.Features.Categories.Queries.GetCategoryById;
public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<CategoryDto>>
{
    private readonly IAppDbContext _context;
    public GetCategoryByIdQueryHandler(IAppDbContext context) => _context = context;
    public async Task<Result<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _context.Categories.AsNoTracking().Where(x => x.Id == request.Id).Select(x => new CategoryDto { Id = x.Id, Name = x.Name, Description = x.Description, CreatedDate = x.CreatedDate }).FirstOrDefaultAsync(cancellationToken);
        return item is null ? Result<CategoryDto>.NotFound("Category not found") : Result<CategoryDto>.Ok(item);
    }
}
