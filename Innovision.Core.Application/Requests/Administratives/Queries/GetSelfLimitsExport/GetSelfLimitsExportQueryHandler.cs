using Innovision.Core.Application.Requests.ApplicationVersions.Queries.GetSelfLimits;
using Innovision.Core.Common.Models;
using MediatR;
using ReportServices.Infrastructure.Helpers;

namespace Innovision.Core.Application.Requests.Administratives.Queries.GetSelfLimitsExport;

public class GetAdminExclusionsExportQueryHandler(IMediator mediator) : IRequestHandler<GetSelfLimitsExportQuery, SelfLimitsFile>
{
  private readonly IMediator _mediator = mediator;

  public async Task<SelfLimitsFile> Handle(GetSelfLimitsExportQuery request, CancellationToken cancellationToken)
  {
    // reuse the GetSelfLimitsQuery handler to get data
    var vm = await _mediator.Send(new GetSelfLimitsQuery(
        new PagedQuery { PageSize = -1 },
        request.Status
        ), cancellationToken);

    var columnHeaders = new Dictionary<string, string>
        {
            { "Fullname", "Name" },
            { "AmountLimit", "Limit amount per bet" },
            { "CreatedOn", "Set Datetime" }
        };

    var exportData = vm.SelfLimits.Select(account => new
    {
      Fullname = account.FullName,
      AmountLimit = account.AmountLimit,
      CreatedOn = ParseDate(account.CreatedOn.Date)
    }).ToList();

    var workbook = ClosedXmlExcelBuilder.ExportExcelToBase64(exportData, columnHeaders);
    var fileName = $"Self_Limits_{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}.xlsx";

    return new SelfLimitsFile(fileName, workbook);
  }

  private string ParseDate(DateTime? dateTime)
  {
      if (!dateTime.HasValue)
          return string.Empty;

      var parsedDate = dateTime.Value;
      return parsedDate.ToString("MMM d, yyyy, h:mm:ss tt");
  }
}