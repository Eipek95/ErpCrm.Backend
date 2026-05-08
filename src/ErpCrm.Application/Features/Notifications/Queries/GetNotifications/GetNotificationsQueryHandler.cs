using ErpCrm.Application.Common.Interfaces;
using ErpCrm.Application.Common.Pagination;
using ErpCrm.Application.Common.Results;
using ErpCrm.Application.Features.Notifications.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace ErpCrm.Application.Features.Notifications.Queries.GetNotifications;
public class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, Result<PagedResult<NotificationDto>>>
{
    private readonly IAppDbContext _context;
    public GetNotificationsQueryHandler(IAppDbContext context) => _context = context;
    public async Task<Result<PagedResult<NotificationDto>>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Notifications.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            query = query.Where(x => x.Title.ToLower().Contains(search) || x.Message.ToLower().Contains(search));
        }
        if (request.UserId.HasValue) query = query.Where(x => x.UserId == request.UserId.Value);
        if (request.IsRead.HasValue) query = query.Where(x => x.IsRead == request.IsRead.Value);
        query = request.SortBy?.ToLower() switch
        {
            "createddate" => request.SortDescending ? query.OrderByDescending(x => x.CreatedDate) : query.OrderBy(x => x.CreatedDate),
            _ => query.OrderByDescending(x => x.CreatedDate)
        };
        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query.Skip(request.Skip).Take(request.PageSize).Select(x => new NotificationDto { Id = x.Id, UserId = x.UserId, Title = x.Title, Message = x.Message, IsRead = x.IsRead, ReadDate = x.ReadDate, CreatedDate = x.CreatedDate }).ToListAsync(cancellationToken);
        return Result<PagedResult<NotificationDto>>.Ok(new PagedResult<NotificationDto> { Items = items, Page = request.Page, PageSize = request.PageSize, TotalCount = totalCount });
    }
}
