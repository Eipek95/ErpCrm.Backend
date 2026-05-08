using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }

    public string CompanyName { get; set; } = null!;
    public string? ContactName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string City { get; set; } = null!;
    public string? District { get; set; }
    public bool IsActive { get; set; }
}