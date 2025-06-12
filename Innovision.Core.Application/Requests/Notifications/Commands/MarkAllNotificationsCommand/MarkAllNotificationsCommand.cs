using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Notifications.Commands.MarkAllNotificationsCommand;

public class MarkAllNotificationsCommand : IRequest<ApiResponse<bool>>
{
  public long AccountInfoId { get; set; }
  public bool IsRead { get; set; }
}
