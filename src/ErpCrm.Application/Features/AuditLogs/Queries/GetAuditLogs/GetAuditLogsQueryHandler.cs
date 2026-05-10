using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.AuditLogs.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace ErpCrm.Application.Features.AuditLogs.Queries.GetAuditLogs;
public class GetAuditLogsQueryHandler : IRequestHandler<GetAuditLogsQuery, Result<PagedResult<AuditLogDto>>>
{
    private readonly IAppDbContext _context;
    public GetAuditLogsQueryHandler(IAppDbContext context) => _context = context;
    public async Task<Result<PagedResult<AuditLogDto>>> Handle(GetAuditLogsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.AuditLogs.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            query = query.Where(x => x.Action.ToLower().Contains(search) || x.EntityName.ToLower().Contains(search) || (x.IpAddress != null && x.IpAddress.ToLower().Contains(search)));
        }
        if (request.UserId.HasValue) query = query.Where(x => x.UserId == request.UserId.Value);
        if (!string.IsNullOrWhiteSpace(request.Action)) query = query.Where(x => x.Action == request.Action);
        if (!string.IsNullOrWhiteSpace(request.EntityName)) query = query.Where(x => x.EntityName == request.EntityName);
        query = request.SortBy?.ToLower() switch
        {
            "createddate" => request.SortDescending ? query.OrderByDescending(x => x.CreatedDate) : query.OrderBy(x => x.CreatedDate),
            _ => query.OrderByDescending(x => x.CreatedDate)
        };
        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query.Skip(request.Skip).Take(request.PageSize).Select(x => new AuditLogDto { Id = x.Id, UserId = x.UserId, Action = x.Action, EntityName = x.EntityName, OldValues = x.OldValues, NewValues = x.NewValues, IPAddress = x.IpAddress, CreatedDate = x.CreatedDate }).ToListAsync(cancellationToken);
        return Result<PagedResult<AuditLogDto>>.Ok(new PagedResult<AuditLogDto> { Items = items, Page = request.Page, PageSize = request.PageSize, TotalCount = totalCount });
    }
}
