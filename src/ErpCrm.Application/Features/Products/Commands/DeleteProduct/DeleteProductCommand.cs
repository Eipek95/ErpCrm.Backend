using ErpCrm.Application.Common.Results;
using MediatR;
namespace ErpCrm.Application.Features.Products.Commands.DeleteProduct;
public record DeleteProductCommand(int Id) : IRequest<Result<bool>>;
