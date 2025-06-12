using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity;

public class Notification : AuditableEntity
{
  public long NotificationId { get; set; }
  public long AccountInfoId { get; set; }
  public int NotificationTypeId { get; set; }
  public bool IsRead { get; set; } = false;
  public string Title { get; set; }
  public string Description { get; set; }
  public string RedirectUrl { get; set; }

  public virtual Account Account { get; set; }
  public virtual NotificationType NotificationType { get; set; }
}