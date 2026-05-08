using ErpCrm.Application.Common.Results;
using MediatR;

namespace ErpCrm.Application.Features.Stocks.Commands.DeleteStock;

public record DeleteStockCommand(int Id) : IRequest<Result<bool>>;
