using Innovision.Core.Application.Requests.Notifications.Queries;
using MediatR;

namespace Innovision.Core.Application.Requests.Notifications.Commands.UpdateNotificationCommand;

public class UpdateNotificationCommand : IRequest<NotificationDto>
{
  public long AccountInfoId { get; set; }
  public long NotificationId { get; set; }
  public bool IsRead { get; set; }
}
