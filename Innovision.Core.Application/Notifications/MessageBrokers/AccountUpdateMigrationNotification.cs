using Innovision.Core.Infrastructure.Interfaces;
using Innovision.Core.Infrastructure.MessageBrokerClient.Models;
using MediatR;

namespace Innovision.Core.Application.Notifications.MessageBrokers;

public record AccountUpdateMigrationNotification(Guid AccountObjectId) : INotification;

public class AccountUpdateMigrationNotificationHandler(IMessageBrokerClientApi messageBrokerApi) : INotificationHandler<AccountUpdateMigrationNotification>
{
    private readonly IMessageBrokerClientApi _messageBrokerApi = messageBrokerApi;

    public async Task Handle(AccountUpdateMigrationNotification notification, CancellationToken cancellationToken)
    {
        try
        {
            var record = new CreateRecordRequest<UserMigrationInfo>("migrate-account-report", new UserMigrationInfo(notification.AccountObjectId));
            _ = await _messageBrokerApi.AddRecordAsync(record, cancellationToken);
        }
        catch
        {

        }
    }
}