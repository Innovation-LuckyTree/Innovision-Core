using Innovision.Core.Application.Requests.ApplicationVersions.Queries.GetAdminExclusions;
using Innovision.Core.Common.Models;
using MediatR;
using ReportServices.Infrastructure.Helpers;

namespace Innovision.Core.Application.Requests.Administratives.Queries.GetAdminExclusionsExport;

public class GetAdminExclusionsExportQueryHandler(IMediator mediator) : IRequestHandler<GetAdminExclusionsExportQuery, AdminExclusionsFile>
{
  private readonly IMediator _mediator = mediator;

  public async Task<AdminExclusionsFile> Handle(GetAdminExclusionsExportQuery request, CancellationToken cancellationToken)
  {
    // reuse the GetAdminExclusionsQuery handler to get data
    var vm = await _mediator.Send(new GetAdminExclusionsQuery(
        new PagedQuery { PageSize = -1 },
        request.Status
        ), cancellationToken);

    var columnHeaders = new Dictionary<string, string>
        {
            { "Fullname", "Name" },
            { "Time", "Time" },
            { "Date", "Date" },
            { "TimeLeft", "Time Left" },
            { "GameType", "Game Type" }
        };

    var exportData = vm.AdministrativeExclusions.Select(account => new
    {
      Fullname = account.FullName,
      Time = account.CreatedOn.TimeOfDay, // might need to parse
      Date = account.CreatedOn.Date, // might need to parse
      TimeLeft = account.TimeLeft,
      GameType = account.GameType
    }).ToList();

    var workbook = ClosedXmlExcelBuilder.ExportExcelToBase64(exportData, columnHeaders);
    var fileName = $"Admin_Exclusions_{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}.xlsx";

    return new AdminExclusionsFile(fileName, workbook);
  }
}