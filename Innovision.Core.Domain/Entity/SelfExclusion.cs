using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity;

public partial class SelfExclusion : AuditableEntity
{
    public int SelfExclusionId { get; set; }
    public long AccountId { get; set; }
    public bool IsIndefinite { get; set; } = false;
    public  DateTimeOffset? DateStart { get; set; }
    public  DateTimeOffset? DateEnd { get; set; }
    public int Status { get; set; } = 1; //1 - for active

    public virtual Account Account { get; set; }
}
