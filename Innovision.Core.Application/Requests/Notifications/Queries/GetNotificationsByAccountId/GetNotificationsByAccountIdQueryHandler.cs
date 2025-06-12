using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Branches.Queries;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Notifications.Queries.GetNotificationsByAccountId;

public class GetNotificationsByAccountIdQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetNotificationsByAccountIdQuery, NotificationVm>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<NotificationVm> Handle(GetNotificationsByAccountIdQuery request, CancellationToken cancellationToken)
    {
        var notificationsQuery = _coreDbContext.Notifications
            .Where(n => n.AccountInfoId == request.AccountInfoId)
            .OrderByDescending(m => m.NotificationId).AsQueryable();

        var totalCount = notificationsQuery.Count();

        if (request.IsRead.HasValue)
            notificationsQuery = notificationsQuery.Where(n => n.IsRead == request.IsRead);

        if (request.PagedQuery != null)
            notificationsQuery = FilterQuery(notificationsQuery, request.PagedQuery);

        var notifications = await notificationsQuery
            .ProjectTo<NotificationDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var notificationVm = new NotificationVm(notifications)
        {
            Total = totalCount,
            PageNumber = (request.PagedQuery != null) ? request.PagedQuery.PageNumber : 1,
            PageSize = (request.PagedQuery != null) ? request.PagedQuery.PageSize : notificationsQuery.Count(),
            TotalUnreadCount = notifications.Where(m => !m.IsRead).Count(),
            TotalReadCount = notifications.Where(m => m.IsRead).Count()
        };

        return notificationVm;
    }

    public IQueryable<Notification> FilterQuery(IQueryable<Notification> query, PagedQuery pagedQuery)
    {
        if (!string.IsNullOrEmpty(pagedQuery.Search))
            query = query.Where(q => q.Title.ToLower().Contains(pagedQuery.Search.ToLower()));

        if (pagedQuery.PageNumber > 0)
            query = query.Skip((pagedQuery.PageNumber) * pagedQuery.PageSize);

        query = query.Take(pagedQuery.PageSize);

        return query;
    }
}
