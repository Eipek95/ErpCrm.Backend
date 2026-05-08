using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.ProductVariants.Commands.DeleteProductVariant;

public record DeleteProductVariantCommand(int Id) : IRequest<Result<bool>>;
