using Innovision.Core.Infrastructure.Interfaces;
using Innovision.Core.Infrastructure.MessageBrokerClient.Models;
using MediatR;

namespace Innovision.Core.Application.Notifications.MessageBrokers;

public record AddOrderMigrationNotification(long OrderId, int GameId, string GameName, long PlayerAccountId,
    string TransactionNo, decimal TotalAmount, int TotalNoOfItems, DateTimeOffset DateOfTransaction, bool IsBonus) : INotification;

public class AddOrderMigrationNotificationHandler(IMessageBrokerClientApi messageBrokerApi) : INotificationHandler<AddOrderMigrationNotification>
{
    private readonly IMessageBrokerClientApi _messageBrokerApi = messageBrokerApi;

    public async Task Handle(AddOrderMigrationNotification notif, CancellationToken cancellationToken)
    {
        try
        {
            var record = new CreateRecordRequest<OrderMigrationInfo>(
                "migrate-order-to-report",
                new OrderMigrationInfo(notif.OrderId, notif.GameId, notif.GameName, notif.PlayerAccountId, notif.TransactionNo, notif.TotalAmount, notif.TotalNoOfItems, notif.DateOfTransaction, notif.IsBonus));
            _ = await _messageBrokerApi.AddRecordAsync(record, cancellationToken);
        }
        catch
        {

        }
    }
}