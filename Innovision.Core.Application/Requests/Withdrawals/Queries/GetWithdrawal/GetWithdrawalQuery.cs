using Innovision.Core.Application.Common;
using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.Withdrawals.Queries.GetWithdrawal;

public class GetWithdrawalQuery : IRequest<ApiResponse<WithdrawalListVm>>
{
    public int? BranchId { get; set; }
    public  DateTimeOffset? DateFrom { get; set; }
    public  DateTimeOffset? DateTo { get; set; }
    public bool? downloadReport { get; set; }
    public PagedQuery? PagedQuery { get; set; }
}
