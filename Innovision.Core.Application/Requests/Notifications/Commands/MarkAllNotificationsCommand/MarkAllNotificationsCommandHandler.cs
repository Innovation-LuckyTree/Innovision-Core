using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Innovision.Core.Infrastructure.WebsocketServices.Models.Requests;
using Innovision.Core.Infrastructure.Interfaces;
using Innovision.Core.Application.Common;

namespace Innovision.Core.Application.Requests.Notifications.Commands.MarkAllNotificationsCommand;

public class MarkAllNotificationsCommandHandler(ICoreDbContext coreDbContext, IWebsocketServicesApi websocketServicesApi) : IRequestHandler<MarkAllNotificationsCommand, ApiResponse<bool>>
{
  private readonly ICoreDbContext _coreDbContext = coreDbContext;
  private readonly IWebsocketServicesApi _websocketServicesApi = websocketServicesApi;

  public async Task<ApiResponse<bool>> Handle(MarkAllNotificationsCommand request, CancellationToken cancellationToken)
  {
    // update all notifications for the given AccountInfoId to whatever is the value of IsRead
    var notifs = await _coreDbContext.Notifications
        .Where(n => n.AccountInfoId == request.AccountInfoId)
        .ToListAsync(cancellationToken);

    foreach (var notif in notifs)
    {
      notif.IsRead = request.IsRead;
    }

    await _coreDbContext.SaveChangesAsync(cancellationToken);

    // trigger broadcasting of new counter after update changes
    var unreadCount = await _coreDbContext.Notifications
        .Where(n => n.AccountInfoId == request.AccountInfoId && !n.IsRead)
        .CountAsync(cancellationToken);

    var newNotifCountToBroadcast = new BroadcastNotificationCountRequest
    {
      AccountId = request.AccountInfoId,
      UnreadCount = unreadCount
    };

    // trigger websocket broadcast
    var broadcast = await _websocketServicesApi.PostNotificationAsync(newNotifCountToBroadcast, cancellationToken);

    return new ApiResponse<bool>() { Data = true };
  }
}