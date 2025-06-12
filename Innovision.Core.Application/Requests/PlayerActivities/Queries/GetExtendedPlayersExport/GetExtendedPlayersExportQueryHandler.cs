using Innovision.Core.Application.Requests.PlayerActivities.Queries.GetExtendedPlayers;
using MediatR;
using ReportServices.Infrastructure.Helpers;

namespace Innovision.Core.Application.Requests.PlayerActivities.Queries.GetExtendedPlayersExport;

public class GetExtendedPlayersExportQueryHandler(IMediator mediator) : IRequestHandler<GetExtendedPlayersExportQuery, ExtendedPlayersFile>
{
  private readonly IMediator _mediator = mediator;

  public async Task<ExtendedPlayersFile> Handle(GetExtendedPlayersExportQuery request, CancellationToken cancellationToken)
  {
    // reuse the GetExtendedPlayersQuery handler to get data
    var vm = await _mediator.Send(new GetExtendedPlayersQuery(
        null // pass PagedQuery as null to get all data
        ), cancellationToken);

    var columnHeaders = new Dictionary<string, string>
        {
            { "Fullname", "Name" },
            { "MissedDraws", "Missed Draws" },
            { "Extension", "Extension" },
            { "Remaining", "Remaining" }
        };

    var exportData = vm.Data.Results.Select(account => new
    {
      Fullname = account.FullName,
      MissedDraws = account.MissedDraws,
      Extension = account.Extended,
      Remaining = Math.Max(0, account.MissedDraws + account.Extended - (account.StandardMissedDraw ?? 999999999))
    }).ToList();

    var workbook = ClosedXmlExcelBuilder.ExportExcelToBase64(exportData, columnHeaders);
    var fileName = $"Extended_Players_{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}.xlsx";

    return new ExtendedPlayersFile(fileName, workbook);
  }
}
