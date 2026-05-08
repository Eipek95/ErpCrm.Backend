using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.ProductVariants.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.ProductVariants.Queries.GetProductVariantById;

public record GetProductVariantByIdQuery(int Id) : IRequest<Result<ProductVariantDto>>;
