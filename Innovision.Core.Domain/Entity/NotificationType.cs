using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity;

public partial class NotificationType : AuditableEntity
{
  public int NotificationTypeId { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }

  public virtual IEnumerable<Notification> Notifications { get; set; }
}