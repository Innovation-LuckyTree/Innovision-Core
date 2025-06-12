using Innovision.Core.Application.Requests.PlayerActivities.Queries.GetRequiredToPayPlayers;
using MediatR;
using ReportServices.Infrastructure.Helpers;

namespace Innovision.Core.Application.Requests.PlayerActivities.Queries.GetRequiredToPlayPlayersExport;

public class GetRequiredToPlayPlayersExportQueryHandler(IMediator mediator) : IRequestHandler<GetRequiredToPlayPlayersExportQuery, RequiredToPlayPlayersFile>
{
  private readonly IMediator _mediator = mediator;

  public async Task<RequiredToPlayPlayersFile> Handle(GetRequiredToPlayPlayersExportQuery request, CancellationToken cancellationToken)
  {
    // reuse the GetRequiredToPayPlayersQuery handler to get data
    var vm = await _mediator.Send(new GetRequiredToPayPlayersQuery(
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
    var fileName = $"RequiredToPlay_Players_{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}.xlsx";

    return new RequiredToPlayPlayersFile(fileName, workbook);
  }
}
