using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity;

public partial class OrderItem : AuditableEntity
{
    public long OrderItemId { get; set; }
    public long OrderId { get; set; }
    public long AccountInfoId { get; set; }
    public bool Used { get; set; } = false;
    public string Values { get; set; }
    public Guid? ItemId { get; set; } // previously called as BetId
    public int BetItemType { get; set; } // 0-Straight, 1-Shuffle
    public int CompanyGameId { get; set; }
    public decimal AmountBet { get; set; } = 0;
    public decimal ExcessAmount { get; set; } = 0;
    public bool IsBonus { get; set; } = false;
    public bool HasExcessAmount { get; set; } = false;
    public  DateTimeOffset? UsedDate { get; set; } = null;
    public string DrawTime { get; set; }
    public  DateTimeOffset? DrawDate { get; set; }
    public bool IsDeleted { get; set; } = false;

    public virtual Order Order { get; set; }
    public virtual Account AccountInfo { get; set; }
    public virtual JackpotWinner JackpotWinner { get; set; }
}
