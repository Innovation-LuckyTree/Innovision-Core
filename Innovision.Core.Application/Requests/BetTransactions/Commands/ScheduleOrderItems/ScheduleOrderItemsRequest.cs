namespace Innovision.Core.Application.Requests.Orders.Commands.ScheduleBetTransactions;

public class ScheduleBetTransactionsRequest
{
    public long GameScheduleId { get; set; }
    public IEnumerable<long> BetTransactions { get; set; }
}

