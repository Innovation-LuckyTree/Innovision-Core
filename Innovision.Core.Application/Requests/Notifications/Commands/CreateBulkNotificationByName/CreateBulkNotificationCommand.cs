using Innovision.Core.Application.Requests.Notifications.Queries;
using MediatR;

namespace Innovision.Core.Application.Requests.Notifications.Commands.CreateBulkNotificationByName;

public class CreateBulkNotificationByNameCommand : IRequest<IEnumerable<NotificationDto>>
{
  public IEnumerable<AccountNotificationInfo> AccountNotifications { get; set; }
}
