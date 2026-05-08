using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommandHandler
    : IRequestHandler<DeleteCustomerCommand, Result<bool>>
{
    private readonly IAppDbContext _context;

    public DeleteCustomerCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(
        DeleteCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (customer is null)
            return Result<bool>.NotFound("Customer not found");

        customer.IsDeleted = true;
        customer.DeletedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Ok(true, "Customer deleted successfully");
    }
}