using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Customers.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpCrm.Application.Features.Customers.Queries.GetCustomerById;

public class GetCustomerByIdQueryHandler
    : IRequestHandler<GetCustomerByIdQuery, Result<CustomerDto>>
{
    private readonly IAppDbContext _context;

    public GetCustomerByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<CustomerDto>> Handle(
        GetCustomerByIdQuery request,
        CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(x => new CustomerDto
            {
                Id = x.Id,
                CompanyName = x.CompanyName,
                ContactName = x.ContactName,
                Email = x.Email,
                Phone = x.Phone,
                City = x.City,
                District = x.District,
                IsActive = x.IsActive,
                CreatedDate = x.CreatedDate
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (customer is null)
            return Result<CustomerDto>.NotFound("Customer not found");

        return Result<CustomerDto>.Ok(customer);
    }
}