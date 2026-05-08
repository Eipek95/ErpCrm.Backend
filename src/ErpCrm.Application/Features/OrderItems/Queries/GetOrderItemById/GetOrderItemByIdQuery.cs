using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.OrderItems.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.OrderItems.Queries.GetOrderItemById;

public record GetOrderItemByIdQuery(int Id) : IRequest<Result<OrderItemDto>>;
