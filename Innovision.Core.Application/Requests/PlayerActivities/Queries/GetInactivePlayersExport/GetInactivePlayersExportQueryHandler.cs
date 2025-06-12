using Innovision.Core.Application.Requests.PlayerActivities.Queries.GetInactivePlayers;
using MediatR;
using ReportServices.Infrastructure.Helpers;

namespace Innovision.Core.Application.Requests.PlayerActivities.Queries.GetInactivePlayersExport;

public class GetInactivePlayersExportQueryHandler(IMediator mediator) : IRequestHandler<GetInactivePlayersExportQuery, InactivePlayersFile>
{
  private readonly IMediator _mediator = mediator;

  public async Task<InactivePlayersFile> Handle(GetInactivePlayersExportQuery request, CancellationToken cancellationToken)
  {
    // reuse the GetInactivePlayersQuery handler to get data
    var vm = await _mediator.Send(new GetInactivePlayersQuery(
        null // pass PagedQuery as null to get all data
        ), cancellationToken);

    var columnHeaders = new Dictionary<string, string>
        {
            { "Fullname", "Name" },
            { "MissedDraws", "Missed Draws" },
            { "ContactNumber", "Mobile Number" },
            { "StartOfInactivity", "Start of Inactivity" }
        };

    var exportData = vm.Data.Results.Select(account => new
    {
      Fullname = account.FullName,
      MissedDraws = account.MissedDraws,
      ContactNumber = account.MobileNumber,
      StartOfInactivity = ParseDate(account.LastModified.Value.Date)
    }).ToList();

    var workbook = ClosedXmlExcelBuilder.ExportExcelToBase64(exportData, columnHeaders);
    var fileName = $"Inactive_Players_{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}.xlsx";

    return new InactivePlayersFile(fileName, workbook);
  }

  private string ParseDate(DateTime? dateTime)
  {
      if (!dateTime.HasValue)
          return string.Empty;

      var parsedDate = dateTime.Value;
      return parsedDate.ToString("MMM d, yyyy, h:mm:ss tt");
  }
}