using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Innovision.Core.Application.Notifications.AccountNotifications;

public record CreateNotificationByNameNotification(long AccountInfoId, int NotificationTypeId, string Name, string MobileNumber) : INotification
{
    public string[] Params { get; set; }
}

public class CreateNotificationByNameNotificationHandler(ICoreDbContext coreDbContext, IMediator mediator, INotificationMessageVm notificationMessage, INotificationMessageVm notificationVm, ILogger<CreateNotificationByNameNotificationHandler> logger) : INotificationHandler<CreateNotificationByNameNotification>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMediator _mediator = mediator;
    private readonly INotificationMessageVm _notificationMessage = notificationMessage;
    private readonly ILogger<CreateNotificationByNameNotificationHandler> _logger = logger;

    public async Task Handle(CreateNotificationByNameNotification notification, CancellationToken cancellationToken)
    {
        try
        {
            var notificationVm = _notificationMessage.GetNotificationMessageByName(notification.Name);

            if (notificationVm == null)
            {
                return;
            }

            var description = notificationVm.Notifications;

            if ((notification.Params?.Count() ?? 0) > 0)
            {
                description = string.Format(notificationVm.Notifications, notification.Params);
            }

            Notification notif = new()
            {
                AccountInfoId = notification.AccountInfoId,
                NotificationTypeId = notification.NotificationTypeId,
                Title = notificationVm.Title,
                Description = description,
                RedirectUrl = notificationVm.Url,
                CreatedOn = DateTime.UtcNow
            };

            _coreDbContext.Notifications.Add(notif);

            await _coreDbContext.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new BroadcastCountNotification(notification.AccountInfoId), cancellationToken).ConfigureAwait(false);

            //await _mediator.Publish(new SmsQueueingNotification(notification.MobileNumber, $"{notificationVm.Title}\n ${notificationVm.Notifications}"), cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to process notification!");
        }
    }
}
