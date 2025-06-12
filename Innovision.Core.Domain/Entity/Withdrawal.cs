using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity;

public class Withdrawal : AuditableEntity
{
    public long TransactionId { get; set; }
    public string TransactionNo { get; set; }
    public string TransactionType { get; set; }
    public long AccountInfoId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; }
    public string Remarks { get; set; }
    public int Status { get; set; } // WalletWithdrawalStatusId
    public int? BankReferenceId { get; set; }
    public string? BankInfo { get; set; }
    public string? ImageProof { get; set; }
    public  DateTimeOffset TransactionDate { get; set; } = DateTime.UtcNow;
    public int NotificationStatus { get; set; } = -1;
    public virtual Account AccountInfo { get; set; }
    public virtual WithdrawalStatus WithdrawalStatus { get; set; }
    public virtual BankReference BankReference { get; set; }
}
