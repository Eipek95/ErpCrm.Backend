using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler
    : IRequestHandler<CreateCustomerCommand, Result<int>>
    {
        private readonly IAppDbContext _context;

        public CreateCustomerCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<int>> Handle(
            CreateCustomerCommand request,
            CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                CompanyName = request.CompanyName,
                ContactName = request.ContactName,
                Email = request.Email,
                Phone = request.Phone,
                City = request.City,
                District = request.District,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            await _context.Customers.AddAsync(customer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<int>.Created(customer.Id, "Customer created successfully");
        }
    }
}
