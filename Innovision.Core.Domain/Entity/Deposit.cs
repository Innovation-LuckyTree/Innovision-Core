using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity;

public class Deposit : AuditableEntity
{
    public long DepositId { get; set; }
    public string TransactionNo { get; set; }
    public string TransactionType { get; set; }
    public long AccountInfoId { get; set; }
    public decimal Amount { get; set; }
    public int PaymentMethodId { get; set; }
    public int DepositStatusId { get; set; }
    public string Remarks { get; set; }
    public  DateTimeOffset? TransactionDate { get; set; } = null;

    public virtual Account AccountInfo { get; set; }
    public virtual PaymentMethod PaymentMethod { get; set; }
    public virtual DepositStatus DepositStatus { get; set; }
}