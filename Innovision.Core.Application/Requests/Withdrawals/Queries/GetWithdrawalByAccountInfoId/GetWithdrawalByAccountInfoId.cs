using Innovision.Core.Application.Common;
using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.Withdrawals.Queries.GetWithdrawalByAccountInfoId;

public class GetWithdrawalByAccountInfoIdQuery : IRequest<ApiResponse<WithdrawalListVm>>
{
    public long AccountInfoId { get; set; }
    public PagedQuery? PagedQuery { get; set; }
}
