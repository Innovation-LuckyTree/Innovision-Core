using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.Deposits.Queries.SearchDepositList;

public class SearchDepositListQuery : IRequest<DepositVm>
{
    public int? BranchId { get; set; }
    public  DateTimeOffset? DateFrom { get; set; }
    public  DateTimeOffset? DateTo { get; set; }
    public bool? downloadReport { get; set; }
    public PagedQuery? PagedQuery { get; set; }
}
