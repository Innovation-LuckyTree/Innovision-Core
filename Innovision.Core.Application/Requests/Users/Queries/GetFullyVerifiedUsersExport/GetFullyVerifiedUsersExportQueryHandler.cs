using Innovision.Core.Application.Requests.Users.Queries.GetFullyVerifiedUsers;
using MediatR;
using ReportServices.Infrastructure.Helpers;

namespace Innovision.Core.Application.Requests.Users.Queries.GetFullyVerifiedUsersExport;

public class GetFullyVerifiedUsersExportQueryHandler(IMediator mediator) : IRequestHandler<GetFullyVerifiedUsersExportQuery, FullyVerifiedUsersFile>
{
  private readonly IMediator _mediator = mediator;

  public async Task<FullyVerifiedUsersFile> Handle(GetFullyVerifiedUsersExportQuery request, CancellationToken cancellationToken)
  {
    // reuse the GetFullyVerifiedUsersQuery handler to get data
    var vm = await _mediator.Send(new GetFullyVerifiedUsersQuery(
        request.CompanyId,
        null, // pass PagedQuery as null to get all data
        request.DateFrom,
        request.DateTo
        ), cancellationToken);

    var columnHeaders = new Dictionary<string, string>
        {
            { "Fullname", "Name" },
            { "ContactNumber", "Mobile Number" },
            { "CreatedOn", "Registration Date" },
            { "LastModified", "Approved Date" }
        };

    var exportData = vm.Data.Results.Select(account => new
    {
      Fullname = account.Fullname,
      ContactNumber = account.ContactNumber,
      CreatedOn = ParseDate(account.CreatedOn.Value.DateTime),
      LastModified = ParseDate(account.LastModified.Value.DateTime)
    }).ToList();

    var workbook = ClosedXmlExcelBuilder.ExportExcelToBase64(exportData, columnHeaders);
    var fileName = $"Fully_Verified_Players_{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}.xlsx";

    return new FullyVerifiedUsersFile(fileName, workbook);
  }

  private string ParseDate(DateTime? dateTime)
  {
      if (!dateTime.HasValue)
          return string.Empty;

      var parsedDate = dateTime.Value;
      return parsedDate.ToString("MMM d, yyyy, h:mm:ss tt");
  }
}