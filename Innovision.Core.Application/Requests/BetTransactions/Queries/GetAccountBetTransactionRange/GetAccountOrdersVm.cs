namespace Innovision.Core.Application.Requests.BetTransactions.Queries.GetAccountBetTransactionRange;

public class BetTransactionVm
{
    public int Total { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public List<BetTransactionDto> BetTransactions { get; set; }
}