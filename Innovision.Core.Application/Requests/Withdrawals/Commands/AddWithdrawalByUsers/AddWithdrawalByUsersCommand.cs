using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Withdrawals.Commands.AddWithdrawalByUsers;
public class AddWithdrawalByUsersCommand : IRequest<ApiResponse<AccountWithdrawalDto>>
{
    public long AccountId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; }
}
