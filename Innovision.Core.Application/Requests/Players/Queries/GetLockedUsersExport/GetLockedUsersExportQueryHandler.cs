using Innovision.Core.Application.Requests.Players.Queries.GetLockedUsers;
using MediatR;
using ReportServices.Infrastructure.Helpers;

namespace Innovision.Core.Application.Requests.Players.Queries.GetLockedUsersExport;

public class GetLockedUsersExportQueryHandler(IMediator mediator) : IRequestHandler<GetLockedUsersExportQuery, LockedUsersFile>
{
  private readonly IMediator _mediator = mediator;

  public async Task<LockedUsersFile> Handle(GetLockedUsersExportQuery request, CancellationToken cancellationToken)
  {
    // reuse the GetLockedUsersQuery handler to get data
    var vm = await _mediator.Send(new GetLockedUsersQuery(
        request.CompanyObjectId,
        null // pass PagedQuery as null to get all data
        ), cancellationToken);

    var columnHeaders = new Dictionary<string, string>
        {
            { "Fullname", "Name" },
            { "ContactNumber", "Mobile Number" },
            { "LockedDate", "Date Locked" }
        };

    var exportData = vm.Data.Results.Select(account => new
    {
      Fullname = account.Fullname,
      ContactNumber = account.ContactNumber,
      LockedDate = ParseDate(account.LockedDate.Value.DateTime)
    }).ToList();

    var workbook = ClosedXmlExcelBuilder.ExportExcelToBase64(exportData, columnHeaders);
    var fileName = $"Locked_Users_{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}.xlsx";

    return new LockedUsersFile(fileName, workbook);
  }

  private string ParseDate(DateTime? dateTime)
  {
      if (!dateTime.HasValue)
          return string.Empty;

      var parsedDate = dateTime.Value;
      return parsedDate.ToString("MMM d, yyyy, h:mm:ss tt");
  }
}