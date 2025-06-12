using Innovision.Core.Application.Wallets.Queries;
using MediatR;

namespace Innovision.Core.Application.Wallets.Commands.UpdateDepositSettings;

public record UpdateDepositSettingsCommand(int WalletSettingId) : IRequest<WalletSettingDto>
{
    public decimal InitialMinimumDeposit { get; set; }
    public decimal SubsequentMinimumDeposit { get; set; }
    public decimal MaximumDepositAtOnce { get; set; }
    public decimal MaximumDepositPerDay { get; set; }
}
