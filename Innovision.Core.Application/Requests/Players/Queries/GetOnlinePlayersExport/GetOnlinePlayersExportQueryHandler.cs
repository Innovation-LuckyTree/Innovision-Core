using Innovision.Core.Application.Requests.Players.Queries.GetOnlinePlayers;
using Innovision.Core.Common.Models;
using MediatR;
using ReportServices.Infrastructure.Helpers;

namespace Innovision.Core.Application.Requests.Players.Queries.GetOnlinePlayersExport;

public class GetOnlinePlayersExportQueryHandler(IMediator mediator) : IRequestHandler<GetOnlinePlayersExportQuery, OnlinePlayersFile>
{
  private readonly IMediator _mediator = mediator;

  public async Task<OnlinePlayersFile> Handle(GetOnlinePlayersExportQuery request, CancellationToken cancellationToken)
  {
    // reuse the GetOnlinePlayersQuery handler to get data
    var vm = await _mediator.Send(new GetOnlinePlayersQuery(
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
    var fileName = $"Online_Players_{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}.xlsx";

    return new OnlinePlayersFile(fileName, workbook);
  }
}