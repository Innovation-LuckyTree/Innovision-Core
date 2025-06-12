namespace Innovision.Core.Domain.Entity;

public class WithdrawalStatus
{
    public int WithdrawalStatusId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual IEnumerable<Withdrawal> Withdrawals { get; set; }
}
