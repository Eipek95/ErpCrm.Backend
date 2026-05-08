using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Stocks.Commands.DeleteStock;

public class DeleteStockCommandHandler : IRequestHandler<DeleteStockCommand, Result<bool>>
{
    private readonly IAppDbContext _context;

    public DeleteStockCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(DeleteStockCommand request, CancellationToken cancellationToken)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (stock is null)
            return Result<bool>.NotFound("Stock not found");

        stock.IsDeleted = true;
        stock.DeletedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Ok(true, "Stock deleted successfully");
    }
}
