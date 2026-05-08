using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Products.DTOs;
using MediatR;
namespace ErpCrm.Application.Features.Products.Queries.GetProductById;
public record GetProductByIdQuery(int Id) : IRequest<Result<ProductDto>>;
