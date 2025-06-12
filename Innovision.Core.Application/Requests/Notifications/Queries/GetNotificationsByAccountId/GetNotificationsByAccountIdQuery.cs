using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.Notifications.Queries.GetNotificationsByAccountId;

public record GetNotificationsByAccountIdQuery : IRequest<NotificationVm>
{
    public long AccountInfoId { get; set; }
    public bool? IsRead { get; set; }
    public PagedQuery? PagedQuery { get; set; }
}
