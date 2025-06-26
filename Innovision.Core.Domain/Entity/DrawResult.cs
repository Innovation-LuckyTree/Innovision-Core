
using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity;

public partial class DrawResult : AuditableEntity
{
    public long DrawResultId { get; set; }
    public long RoundId { get; set; }
    public string RoundReference { get; set; }
    public DateOnly Date { get; set; }
    public int GameId { get; set; }
    public DateTime StartCutoff { get; set; }
    public DateTime EndCutoff { get; set; }
    public long StartCutoffEpoch { get; set; }
    public long EndCutoffEpoch { get; set; }
    public int BettingTime { get; set; }
    public int NoOfWinners { get; set; }
    public decimal WinAmount { get; set; }
    public int TotalBetCount { get; set; }
    public decimal TotalBetAmount { get; set; }
    public DateTime DrawDate { get; set; }

    public virtual Game Game { get; set; }
    public virtual ICollection<BetTransaction> BetTransactions { get; set; }
}