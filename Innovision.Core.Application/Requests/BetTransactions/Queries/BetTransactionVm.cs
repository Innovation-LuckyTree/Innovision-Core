namespace Innovision.Core.Application.Requests.Orders.Queries;

public record BetTransactionVm(IEnumerable<BetTransactionDto> BetTransactions)
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