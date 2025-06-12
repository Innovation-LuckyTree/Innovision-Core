using Innovision.Core.Application.Wallets.Queries;
using MediatR;

namespace Innovision.Core.Application.Wallets.Commands.CreateWalletSettings;

public class CreateWalletSettingsCommand : IRequest<WalletSettingDto>
{
    public decimal InitialMinimumDeposit { get; set; }
    public decimal SubsequentMinimumDeposit { get; set; }
    public decimal MaximumDepositAtOnce { get; set; }
    public decimal MaximumDepositPerDay { get; set; }
    public decimal InitialMinimumWithdraw { get; set; }
    public decimal SubsequentMinimumWithdraw { get; set; }
    public decimal MaximumWithdrawAtOnce { get; set; }
    public decimal MaximumWithdrawPerDay { get; set; }
}
