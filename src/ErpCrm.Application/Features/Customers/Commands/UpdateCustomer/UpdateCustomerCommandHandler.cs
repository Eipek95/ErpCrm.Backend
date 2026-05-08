using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandler
    : IRequestHandler<UpdateCustomerCommand, Result<bool>>
{
    private readonly IAppDbContext _context;

    public UpdateCustomerCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(
        UpdateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (customer is null)
            return Result<bool>.NotFound("Customer not found");

        customer.CompanyName = request.CompanyName;
        customer.ContactName = request.ContactName;
        customer.Email = request.Email;
        customer.Phone = request.Phone;
        customer.City = request.City;
        customer.District = request.District;
        customer.IsActive = request.IsActive;
        customer.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Ok(true, "Customer updated successfully");
    }
}