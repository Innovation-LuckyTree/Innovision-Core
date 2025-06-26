namespace Innovision.Core.Application.Requests.Orders.Queries.GetBetTransactionsList;

public record BetTransactionDetailVm(IEnumerable<BetTransactionDetailDto> BetTransactions, long OffsetBetTransactionId)
{
    public int Count
    {
        get
        {
            return BetTransactions?.Count() ?? 0;
        }
    }
}
