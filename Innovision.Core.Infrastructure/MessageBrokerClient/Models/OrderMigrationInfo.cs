namespace Innovision.Core.Infrastructure.MessageBrokerClient.Models;

public record OrderMigrationInfo(long OrderId, int GameId, string GameName, long PlayerAccountId,
    string TransactionNo, decimal TotalAmount, int TotalNoOfItems, DateTimeOffset DateOfTransaction, bool IsBonus);
