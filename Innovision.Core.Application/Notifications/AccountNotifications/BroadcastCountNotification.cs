using Innovision.Core.Application.Interfaces;
using Innovision.Core.Infrastructure.Interfaces;
using Innovision.Core.Infrastructure.WebsocketServices.Models.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Notifications.AccountNotifications;

public record BroadcastCountNotification(long AccountInfoId) : INotification;

public class BroadcastCountNotificationHandler(ICoreDbContext coreDbContext, IWebsocketServicesApi websocketServicesApi) : INotificationHandler<BroadcastCountNotification>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IWebsocketServicesApi _websocketServicesApi = websocketServicesApi;

    public async Task Handle(BroadcastCountNotification notification, CancellationToken cancellationToken)
    {
        // unread notification count for user (with AccountInfoId)
        var unreadCount = await _coreDbContext.Notifications
        .Where(n => n.AccountInfoId == notification.AccountInfoId && !n.IsRead)
        .CountAsync(cancellationToken);

        var newNotifCountToBroadcast = new BroadcastNotificationCountRequest
        {
            AccountId = notification.AccountInfoId,
            UnreadCount = unreadCount
        };

        // trigger websocket broadcast
        var broadcast = await _websocketServicesApi.PostNotificationAsync(newNotifCountToBroadcast, cancellationToken);
    }
}