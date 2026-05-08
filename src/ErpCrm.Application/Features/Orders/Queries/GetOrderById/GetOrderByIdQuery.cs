using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Orders.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(int Id) : IRequest<Result<OrderDto>>;
