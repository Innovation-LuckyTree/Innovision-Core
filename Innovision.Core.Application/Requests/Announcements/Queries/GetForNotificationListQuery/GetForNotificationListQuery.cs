using MediatR;

namespace Innovision.Core.Application.Requests.Announcements.Queries.GetForNotificationListQuery;

public record GetForNotificationListQuery : IRequest<AnnouncementVm>
{
}
