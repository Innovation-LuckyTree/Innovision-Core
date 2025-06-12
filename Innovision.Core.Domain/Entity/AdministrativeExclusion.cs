using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity;

public partial class AdministrativeExclusion : AuditableEntity
{
    public int AdministrativeExclusionId { get; set; }
    public long AccountId { get; set; }
    public int DayDuration { get; set; }
    public TimeSpan TimeDuration { get; set; }
    public  DateTimeOffset DateExpiry { get; set; }
    public int Status { get; set; } = 1; // set to active

    public virtual Account Account { get; set; }
}
