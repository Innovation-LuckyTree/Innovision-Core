using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Withdrawals.Commands.AddWithdrawalByAccounting;

public class AddWithdrawalByAccountingCommand : IRequest<ApiResponse<WithdrawalDto>>
{
    public long AccountInfoId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; }
    public int Status { get; set; }
    public BankInfo? BankInfo { get; set; }
}

public class BankInfo {
    public int BankReferenceId { get; set; }
    public string AccountName { get; set; }
    public string AccountNumber { get; set; }
}
