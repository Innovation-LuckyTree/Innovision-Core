using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity;

public class Announcement : AuditableEntity
{
    public long AnnouncementId { get; set; }
    public int? BranchId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string SendTo { get; set; }
    public  DateTimeOffset? StartDate { get; set; }
    public  DateTimeOffset? EndDate { get; set; }
    public bool IsBanner { get; set; }
    public int Status { get; set; } = 0; // 0 = pending, 1 = stopped in mobile, 2 = notified

    public virtual Branch Branch { get; set; }
}