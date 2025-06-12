using Innovision.Core.Application.Wallets.Queries;
using MediatR;

namespace Innovision.Core.Application.Wallets.Commands.UpdateWithdrawalSettings;

public record UpdateWithdrawalSettingsCommand(int WalletSettingId) : IRequest<WalletSettingDto>
{
    public decimal InitialMinimumWithdraw { get; set; }
    public decimal SubsequentMinimumWithdraw { get; set; }
    public decimal MaximumWithdrawAtOnce { get; set; }
    public decimal MaximumWithdrawPerDay { get; set; }
}
