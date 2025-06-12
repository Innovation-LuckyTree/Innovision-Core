using Innovision.Core.Infrastructure.Interfaces;
using Innovision.Core.Infrastructure.MessageBrokerClient.Models;
using MediatR;

namespace Innovision.Core.Application.Notifications.MessageBrokers;

public record AddBetTransactionNotification(IEnumerable<long> OrderItemIds) : INotification;

public class AddBetTransactionNotificationHandler(IMessageBrokerClientApi messageBrokerApi) : INotificationHandler<AddBetTransactionNotification>
{
    private readonly IMessageBrokerClientApi _messageBrokerApi = messageBrokerApi;

    public async Task Handle(AddBetTransactionNotification notification, CancellationToken cancellationToken)
    {
        try
        {
            foreach (var orderItem in notification.OrderItemIds)
            {
                var record = new CreateRecordRequest<BetItemMigration>("migrate-bet-detail", new BetItemMigration(orderItem));

                _ = await _messageBrokerApi.AddRecordAsync(record, cancellationToken);
            }
        }
        catch
        {

        }
    }
}