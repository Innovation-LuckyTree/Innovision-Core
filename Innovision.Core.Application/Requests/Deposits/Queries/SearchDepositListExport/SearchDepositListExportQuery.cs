using MediatR;

namespace Innovision.Core.Application.Requests.Deposits.Queries.SearchDepositListExport;

public record SearchDepositListExportQuery : IRequest<DepositListFile>
{
  public int? BranchId { get; set; }
  public  DateTimeOffset? DateFrom { get; set; }
  public  DateTimeOffset? DateTo { get; set; }
  public bool? DownloadReport { get; set; }
}
