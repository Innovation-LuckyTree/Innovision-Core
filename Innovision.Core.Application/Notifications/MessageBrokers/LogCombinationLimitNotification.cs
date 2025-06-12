using Innovision.Core.Application.Common.Models;
using Innovision.Core.Infrastructure.Interfaces;
using Innovision.Core.Infrastructure.MessageBrokerClient.Models;
using MediatR;

namespace Innovision.Core.Application.Notifications.MessageBrokers;

public record LogCombinationLimitNotification(IEnumerable<BetInformation> BetInformations) : INotification;

public class LogCombinationLimitNotificationHandler(IMessageBrokerClientApi messageBrokerApi) : INotificationHandler<LogCombinationLimitNotification>
{
    private readonly IMessageBrokerClientApi _messageBrokerApi = messageBrokerApi;

    public async Task Handle(LogCombinationLimitNotification notification, CancellationToken cancellationToken)
    {
        try
        {
            foreach (var betInfo in notification.BetInformations)
            {
                var record = new CreateRecordRequest<BetInformation>("increment-limit-transaction", betInfo);

                _ = await _messageBrokerApi.AddRecordAsync(record, cancellationToken);
            }
        }
        catch
        {

        }
    }
}