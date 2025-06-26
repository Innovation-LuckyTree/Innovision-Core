using Innovision.Core.Application.Requests.Orders.Queries.GetBetTransactionsList;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetBetTransactionsByIds;

public record BetTransactionDetailsVm(IEnumerable<BetTransactionDetailDto> BetTransactions)
{
    public decimal TotalAmount
    {
        get
        {
            if ((BetTransactions?.Count() ?? 0) > 0)
                return BetTransactions.Sum(o => o.AmountBet);

            return 0;
        }
    }

    public int TotalCount
    {
        get
        {
            return BetTransactions?.Count() ?? 0;
        }
    }
}
