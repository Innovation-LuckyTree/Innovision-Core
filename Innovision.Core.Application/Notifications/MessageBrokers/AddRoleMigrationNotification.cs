using Innovision.Core.Infrastructure.Interfaces;
using Innovision.Core.Infrastructure.MessageBrokerClient.Models;
using MediatR;

namespace Innovision.Core.Application.Notifications.MessageBrokers;

public record AddRoleMigrationNotification(int UserTypeId, string RoleName) : INotification;

public class AddRoleMigrationNotificationHandler(IMessageBrokerClientApi messageBrokerApi) : INotificationHandler<AddRoleMigrationNotification>
{
    private readonly IMessageBrokerClientApi _messageBrokerApi = messageBrokerApi;

    public async Task Handle(AddRoleMigrationNotification notification, CancellationToken cancellationToken)
    {
        try
        {
            var record = new CreateRecordRequest<RoleMigrationInfo>("migrate-role", new RoleMigrationInfo(notification.UserTypeId, notification.RoleName));
            _ = await _messageBrokerApi.AddRecordAsync(record, cancellationToken);
        }
        catch
        {

        }
    }
}