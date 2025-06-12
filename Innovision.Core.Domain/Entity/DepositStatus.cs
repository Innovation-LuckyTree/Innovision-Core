namespace Innovision.Core.Domain.Entity;

public class DepositStatus
{
    public int DepositStatusId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual IEnumerable<Deposit> Deposits { get; set; }
}
