using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.Customers.Commands.DeleteCustomer;

public record DeleteCustomerCommand(int Id) : IRequest<Result<bool>>;