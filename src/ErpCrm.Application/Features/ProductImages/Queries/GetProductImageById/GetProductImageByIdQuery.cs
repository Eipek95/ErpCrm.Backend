using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.ProductImages.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.ProductImages.Queries.GetProductImageById;

public record GetProductImageByIdQuery(int Id) : IRequest<Result<ProductImageDto>>;
