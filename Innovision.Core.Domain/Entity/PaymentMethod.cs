using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity;

public partial class PaymentMethod : AuditableEntity
{
    public int PaymentMethodId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual IEnumerable<Deposit> Deposits { get; set; }
}
