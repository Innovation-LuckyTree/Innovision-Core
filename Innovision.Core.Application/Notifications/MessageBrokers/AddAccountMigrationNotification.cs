using Innovision.Core.Infrastructure.Interfaces;
using Innovision.Core.Infrastructure.MessageBrokerClient.Models;
using MediatR;

namespace Innovision.Core.Application.Notifications.MessageBrokers;

public record AddAccountMigrationNotification(Guid AccountObjectId) : INotification;

public class AddAccountMigrationCommandHandler(IMessageBrokerClientApi messageBrokerApi) : INotificationHandler<AddAccountMigrationNotification>
{
    private readonly IMessageBrokerClientApi _messageBrokerApi = messageBrokerApi;

    public async Task Handle(AddAccountMigrationNotification notification, CancellationToken cancellationToken)
    {
        try
        {
            var record = new CreateRecordRequest<UserMigrationInfo>("inno-migrate-account", new UserMigrationInfo(notification.AccountObjectId));
            _ = await _messageBrokerApi.AddRecordAsync(record, cancellationToken);

            var reportRecord = new CreateRecordRequest<UserMigrationInfo>("migrate-account-report", new UserMigrationInfo(notification.AccountObjectId));
            _ = await _messageBrokerApi.AddRecordAsync(record, cancellationToken);
        }
        catch
        {

        }
    }
}