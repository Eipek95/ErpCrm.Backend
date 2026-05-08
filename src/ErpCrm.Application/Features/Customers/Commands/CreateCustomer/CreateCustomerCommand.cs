using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<Result<int>>
    {
        public string CompanyName { get; set; } = null!;
        public string? ContactName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string City { get; set; } = null!;
        public string? District { get; set; }
    }
}
