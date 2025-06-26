using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Commands.CreateBetTransaction;

public record CreateBetTransactionCommand : IRequest<long>
{
    public long AccountInfoId { get; set; }
    public long ReferenceId { get; set; }
    public long? DrawResultId { get; set; }
    public string RoundReference { get; set; }
    public int GameId { get; set; }
    public string BetValue { get; set; }
    public string TransactionType { get; set; }
    public decimal AmountBet { get; set; } = 0;
    public bool IsBonus { get; set; } = false;
}
