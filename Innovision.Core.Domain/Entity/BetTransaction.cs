using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity;

public partial class BetTransaction : AuditableEntity
{
    public long BetTransactionId { get; set; }
    public long AccountInfoId { get; set; }
    public long ReferenceId { get; set; }
    public long? DrawResultId { get; set; }
    public string RoundReference { get; set; }
    public int GameId { get; set; }
    public string BetValue { get; set; }
    public Guid? ItemId { get; set; }
    public string TransactionType { get; set; }
    public decimal AmountBet { get; set; } = 0;
    public bool IsBonus { get; set; } = false;
    public decimal WinAmount { get; set; } = 0;
    public bool VoidTransaction { get; set; } = false;
    public DateTime? VoidTransactionDate { get; set; }

    public virtual Account AccountInfo { get; set; }
    public virtual Game Game { get; set; }
    public virtual JackpotWinner JackpotWinner { get; set; }
    public virtual DrawResult DrawResult { get; set; }
}
