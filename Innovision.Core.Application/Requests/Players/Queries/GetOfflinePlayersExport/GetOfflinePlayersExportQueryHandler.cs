using Innovision.Core.Application.Requests.Players.Queries.GetOfflinePlayers;
using MediatR;
using ReportServices.Infrastructure.Helpers;

namespace Innovision.Core.Application.Requests.Players.Queries.GetOfflinePlayersExport;

public class GetOfflinePlayersExportQueryHandler(IMediator mediator) : IRequestHandler<GetOfflinePlayersExportQuery, OfflinePlayersFile>
{
  private readonly IMediator _mediator = mediator;

  public async Task<OfflinePlayersFile> Handle(GetOfflinePlayersExportQuery request, CancellationToken cancellationToken)
  {
    // reuse the GetOfflinePlayersQuery handler to get data
    var vm = await _mediator.Send(new GetOfflinePlayersQuery(
        request.CompanyObjId,
        null // pass PagedQuery as null to get all data
        ), cancellationToken);

    var columnHeaders = new Dictionary<string, string>
        {
            { "Fullname", "Name" },
            { "ContactNumber", "Mobile Number" },
            { "RoleName", "User Type" }
        };

    var exportData = vm.Data.Results.Select(account => new
    {
      Fullname = account.Fullname,
      ContactNumber = account.ContactNumber,
      RoleName = account.RoleName
    }).ToList();

    var workbook = ClosedXmlExcelBuilder.ExportExcelToBase64(exportData, columnHeaders);
    var fileName = $"Offline_Players_{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}.xlsx";

    return new OfflinePlayersFile(fileName, workbook);
  }
}