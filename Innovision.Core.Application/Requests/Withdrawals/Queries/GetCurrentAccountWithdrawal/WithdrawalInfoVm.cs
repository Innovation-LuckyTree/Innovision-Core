using Innovision.Core.Application.Common.Constants;

namespace Innovision.Core.Application.Requests.Withdrawals.GetCurrentAccountWithdrawal;

public record WithdrawalInfoVm(IEnumerable<WithdrawalInfoDto> Withdrawals)
{
    public int TotalCount { get; set; }

    public int Count
    {
        get => Withdrawals?.Count() ?? 0;
    }

    public int PendingCount
    {
        get => Withdrawals?.Where(o => o.Status == WalletWithdrawalStatusId.Pending)?.Count() ?? 0;
    }

    public int CompletedCount
    {
        get => Withdrawals?.Where(o => o.Status == WalletWithdrawalStatusId.Complete)?.Count() ?? 0;
    }

    public int DeclinedCount
    {
        get => Withdrawals?.Where(o => o.Status == WalletWithdrawalStatusId.Declined)?.Count() ?? 0;
    }
}