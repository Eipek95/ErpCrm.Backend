using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Dashboard.DTOs;
using ErpCrm.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ErpCrm.Application.Features.Dashboard.Queries.GetMonthlySales;

public class GetMonthlySalesQueryHandler
    : IRequestHandler<GetMonthlySalesQuery, Result<List<MonthlySalesDto>>>
{
    private readonly IAppDbContext _context;

    public GetMonthlySalesQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<MonthlySalesDto>>> Handle(
        GetMonthlySalesQuery request,
        CancellationToken cancellationToken)
    {
        var year = request.Year ?? DateTime.UtcNow.Year;

        var data = await _context.Orders
            .AsNoTracking()
            .Where(x =>
                x.CreatedDate.Year == year &&
                x.Status != OrderStatus.Cancelled)
            .GroupBy(x => new
            {
                x.CreatedDate.Year,
                x.CreatedDate.Month
            })
            .Select(x => new
            {
                x.Key.Year,
                x.Key.Month,
                TotalSales = x.Sum(o => o.TotalAmount),
                OrderCount = x.Count()
            })
            .OrderBy(x => x.Month)
            .ToListAsync(cancellationToken);

        var result = Enumerable.Range(1, 12)
            .Select(month =>
            {
                var item = data.FirstOrDefault(x => x.Month == month);

                return new MonthlySalesDto
                {
                    Year = year,
                    Month = month,
                    MonthName = CultureInfo
                        .GetCultureInfo("tr-TR")
                        .DateTimeFormat
                        .GetMonthName(month),
                    TotalSales = item?.TotalSales ?? 0,
                    OrderCount = item?.OrderCount ?? 0
                };
            })
            .ToList();

        return Result<List<MonthlySalesDto>>.Ok(result);
    }
}