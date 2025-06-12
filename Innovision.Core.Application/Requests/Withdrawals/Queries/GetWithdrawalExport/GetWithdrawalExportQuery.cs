using MediatR;

namespace Innovision.Core.Application.Requests.Withdrawals.Queries.GetWithdrawalExport;

public record GetWithdrawalExportQuery : IRequest<WithdrawalFile>
{
    public int? BranchId { get; set; }
    public  DateTimeOffset? DateFrom { get; set; }
    public  DateTimeOffset? DateTo { get; set; }
    public bool? downloadReport { get; set; }
}
