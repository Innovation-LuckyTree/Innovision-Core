using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;

namespace Innovision.Core.Application.Notifications.AccountNotifications;

public record CreateAccountNotification(long AccountInfoId, int NotificationTypeId, string Title, string Description, string RedirectUrl) : INotification;

public class CreateAccountNotificationHandler(ICoreDbContext coreDbContext, IMediator mediator, INotificationMessageVm notificationMessage) : INotificationHandler<CreateAccountNotification>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMediator _mediator = mediator;

    public async Task Handle(CreateAccountNotification notification, CancellationToken cancellationToken)
    {
        Notification notif = new()
        {
            AccountInfoId = notification.AccountInfoId,
            NotificationTypeId = notification.NotificationTypeId,
            Title = notification.Title,
            Description = notification.Description,
            RedirectUrl = notification.RedirectUrl,
            CreatedOn = DateTime.UtcNow
        };

        _coreDbContext.Notifications.Add(notif);

        await _coreDbContext.SaveChangesAsync(cancellationToken);

        await _mediator.Publish(new BroadcastCountNotification(notification.AccountInfoId), cancellationToken).ConfigureAwait(false);
    }
}
