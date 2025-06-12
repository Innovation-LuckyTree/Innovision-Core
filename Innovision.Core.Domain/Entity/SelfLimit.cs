using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity;

public partial class SelfLimit : AuditableEntity
{
    public int SelfLimitId { get; set; }
    public long AccountId { get; set; }
    public decimal AmountLimit { get; set; }
    public int Status { get; set; } = 1; //1 - for active

    public virtual Account Account { get; set; }
}
