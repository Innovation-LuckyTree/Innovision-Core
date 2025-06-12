using Innovision.Core.Application.Requests.Notifications.Queries;
using MediatR;

namespace Innovision.Core.Application.Requests.Notifications.Commands.CreateAccountsNotificationCommand;

public class CreateAccountsNotificationCommand : IRequest<IEnumerable<NotificationDto>>
{
  public IEnumerable<long> Accounts { get; set; }
  public int NotificationTypeId { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public string RedirectUrl { get; set; }
}
