namespace Innovision.Core.Domain.Entity;

public class BlockedUserHistory
{
  public long BlockedUserHistoryId { get; set; }
  public long AccountInfoId { get; set; }
  public  DateTimeOffset BlockedDate { get; set; } = DateTime.UtcNow;
  public int IsActive { get; set; } = 1;
  public string? Remarks { get; set; }

  public virtual Account Account { get; set; }
}