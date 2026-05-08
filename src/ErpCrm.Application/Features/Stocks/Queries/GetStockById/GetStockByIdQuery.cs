using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Stocks.DTOs;
using MediatR;

namespace ErpCrm.Application.Features.Stocks.Queries.GetStockById;

public record GetStockByIdQuery(int Id) : IRequest<Result<StockDto>>;
