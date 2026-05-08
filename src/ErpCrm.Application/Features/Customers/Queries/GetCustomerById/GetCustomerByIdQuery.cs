using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Customers.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.Customers.Queries.GetCustomerById;

public record GetCustomerByIdQuery(int Id) : IRequest<Result<CustomerDto>>;