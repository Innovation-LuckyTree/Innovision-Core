using Innovision.Core.Application.Requests.BlockedUserHistories.Queries.GetBlockedUsers;
using MediatR;
using ReportServices.Infrastructure.Helpers;

namespace Innovision.Core.Application.Requests.Administratives.Queries.GetBlockedUsersExport;

public class GetBlockedUsersExportQueryHandler(IMediator mediator) : IRequestHandler<GetBlockedUsersExportQuery, BlockedUsersFile>
{
  private readonly IMediator _mediator = mediator;

  public async Task<BlockedUsersFile> Handle(GetBlockedUsersExportQuery request, CancellationToken cancellationToken)
  {
    // reuse the GetBlockedUsersListQuery handler to get data
    var vm = await _mediator.Send(new GetBlockedUsersListQuery
    {
        PagedQuery = null
    }, cancellationToken);

    var columnHeaders = new Dictionary<string, string>
        {
            { "Fullname", "Name" },
            { "MobileNumber", "Mobile Number" },
            { "BlockedDate", "Date Blocked" }
        };

    var exportData = vm.BlockedUsers.Select(account => new
    {
      Fullname = account.Fullname,
      MobileNumber = account.MobileNumber,
      BlockedDate = ParseDate(account.BlockedDate.Date)
    }).ToList();

    var workbook = ClosedXmlExcelBuilder.ExportExcelToBase64(exportData, columnHeaders);
    var fileName = $"Blocked_List_{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}.xlsx";

    return new BlockedUsersFile(fileName, workbook);
  }

  private string ParseDate(DateTime? dateTime)
  {
      if (!dateTime.HasValue)
          return string.Empty;

      var parsedDate = dateTime.Value;
      return parsedDate.ToString("MMM d, yyyy, h:mm:ss tt");
  }
}