using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Customers.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.Customers.Queries.GetCustomers
{
    public class GetCustomersQuery : PagedRequest, IRequest<Result<PagedResult<CustomerDto>>>
    {
        public bool? IsActive { get; set; }
        public string? City { get; set; }
    }
}
