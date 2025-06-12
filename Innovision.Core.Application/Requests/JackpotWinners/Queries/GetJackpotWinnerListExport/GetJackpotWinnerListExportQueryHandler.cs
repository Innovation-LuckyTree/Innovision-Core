using Innovision.Core.Application.Requests.JackpotWinners.Queries.GetJackpotWinnerList;
using MediatR;
using ReportServices.Infrastructure.Helpers;

namespace Innovision.Core.Application.Requests.JackpotWinners.Queries.GetJackpotWinnerListExport;

public class GetJackpotWinnerListExportQueryHandler(IMediator mediator) : IRequestHandler<GetJackpotWinnerListExportQuery, JackpotWinnersFile>
{
  private readonly IMediator _mediator = mediator;

  public async Task<JackpotWinnersFile> Handle(GetJackpotWinnerListExportQuery request, CancellationToken cancellationToken)
  {
    // reuse the GetJackpotWinnerListQuery handler to get data
    var vm = await _mediator.Send(new GetJackpotWinnerListQuery(
        new GetJackpotWinnerListRequest { JackpotStatusId = request.JackpotStatusId, PageQuery = null },
        request.CompanyGameId
        ), cancellationToken);

    var columnHeaders = new Dictionary<string, string>
        {
            { "DisplayName", "Display Name" },
            { "OrderItemId", "Order Item ID" },
            { "ReferenceId", "Reference ID" },
            { "ClaimablePayout", " Claimable Payout" },
            { "Date", "Date" },
            { "DrawTime", "Draw Time" },
            { "Status", "Status" }
        };

    var exportData = vm.Data.ListData.Select(account => new
    {
      DisplayName = account.DisplayName,
      OrderItemId = account.OrderItemId,
      ReferenceId = account.TransactionNumber,
      ClaimablePayout = account.WinAmount,
      Date = account.DrawDateDisplay,
      DrawTime = account.DrawTimeDisplay,
      Status = account.JackpotWinnerStatus == "Pending" ? "Unclaimed" : account.JackpotWinnerStatus
    }).ToList();

    var workbook = ClosedXmlExcelBuilder.ExportExcelToBase64(exportData, columnHeaders);
    var jackpotGameName = request.CompanyGameId == 2 ? "PowerWin" : request.CompanyGameId == 3 ? "TrippleWin" : "MagicWin";
    var fileName = $"{jackpotGameName}_Winners_{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}.xlsx";

    return new JackpotWinnersFile(fileName, workbook);
  }
}
