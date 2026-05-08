using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.ProductImages.Commands.DeleteProductImage;

public record DeleteProductImageCommand(int Id) : IRequest<Result<bool>>;
