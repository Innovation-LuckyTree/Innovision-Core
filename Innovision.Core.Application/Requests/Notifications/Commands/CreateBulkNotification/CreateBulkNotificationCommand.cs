using Innovision.Core.Application.Requests.Notifications.Queries;
using MediatR;

namespace Innovision.Core.Application.Requests.Notifications.Commands.CreateBulkNotification;

public class CreateBulkNotificationCommand : IRequest<IEnumerable<NotificationDto>>
{
  public IEnumerable<AccountNotification> AccountNotifications { get; set; }
}
