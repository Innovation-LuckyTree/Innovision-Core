using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity;

public class WalletSetting : AuditableEntity
{
    public int WalletSettingId { get; set; }
    public decimal InitialMinimumDeposit { get; set; }
    public decimal SubsequentMinimumDeposit { get; set; }
    public decimal MaximumDepositAtOnce { get; set; }
    public decimal MaximumDepositPerDay { get; set; }

    public decimal InitialMinimumWithdraw { get; set; }
    public decimal SubsequentMinimumWithdraw { get; set; }
    public decimal MaximumWithdrawAtOnce { get; set; }
    public decimal MaximumWithdrawPerDay { get; set; }
    public int TaxPercentage { get; set; }
    public decimal TaxableAmount { get; set; }

}
