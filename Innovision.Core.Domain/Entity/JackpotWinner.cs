using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity;

public partial class JackpotWinner : AuditableEntity
{
    public long JackpotWinnerId { get; set; }
    public long AccountInfoId { get; set; }
    public int CompanyGameId { get; set; }
    public string TransactionNo { get; set; }
    public string BetValue { get; set; }
    public long DrawResultId { get; set; }
    public int GameTypeId { get; set; }
    public string GameTypeName { get; set; }
    public int GameId { get; set; }
    public string DrawResult { get; set; }
    public long OrderItemId { get; set; }
    public long GameScheduleId { get; set; }
    public  DateTimeOffset DrawDate { get; set; }
    public TimeSpan DrawTime { get; set; }
    public decimal PrizePoolAmount { get; set; }
    public decimal NetWinAmount { get; set; }
    public decimal GrossWinAmount { get; set; }
    public int NumberOfWinners { get; set; }
    public decimal TotalBetAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TaxPercentage { get; set; }
    public long? ApproverAccountId { get; set; }
    public long? ReleaserAccountId { get; set; }
    public int JackpotWinnerStatusId { get; set; }
    public string Remarks { get; set; }

    public virtual Account Account { get; set; }
    public virtual Account ApproverAccount { get; set; }
    public virtual Account ReleaserAccount { get; set; }
    public virtual Game Game { get; set; }
    public virtual GameType GameType { get; set; }
    public virtual OrderItem OrderItem { get; set; }
    public virtual JackpotWinnerStatus JackpotWinnerStatus { get; set; }
    public virtual IEnumerable<JackpotWinnerAttachment> JackpotWinnerAttachments { get; set; }
}
