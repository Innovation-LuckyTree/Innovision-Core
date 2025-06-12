using Innovision.Core.Application.Requests.Users.Queries.GetSemiVerifiedUsers;
using MediatR;
using ReportServices.Infrastructure.Helpers;

namespace Innovision.Core.Application.Requests.Users.Queries.GetSemiVerifiedUsersExport;

public class GetSemiVerifiedUsersExportQueryHandler(IMediator mediator) : IRequestHandler<GetSemiVerifiedUsersExportQuery, SemiVerifiedUsersFile>
{
  private readonly IMediator _mediator = mediator;

  public async Task<SemiVerifiedUsersFile> Handle(GetSemiVerifiedUsersExportQuery request, CancellationToken cancellationToken)
  {
    // reuse the GetSemiVerifiedUsersQuery handler to get data
    var vm = await _mediator.Send(new GetSemiVerifiedUsersQuery(
        request.CompanyId,
        null, // pass PagedQuery as null to get all data,
        request.DateFrom,
        request.DateTo
        ), cancellationToken);

    var columnHeaders = new Dictionary<string, string>
        {
            { "Fullname", "Name" },
            { "ContactNumber", "Mobile Number" },
            { "CreatedOn", "Registration DateTime" },
            { "LapseDays", "Lapse Days" }
        };

    var exportData = vm.Data.Results.Select(account => new
    {
      Fullname = account.Fullname,
      ContactNumber = account.ContactNumber,
      CreatedOn = ParseDate(account.CreatedOn.Value.DateTime),
      LapseDays = account.LastModified.HasValue && account.CreatedOn.HasValue
          ? (account.LastModified.Value.Date - account.CreatedOn.Value.Date).Days
          : 0 // default to 0 if either value is null
    }).ToList();

    var workbook = ClosedXmlExcelBuilder.ExportExcelToBase64(exportData, columnHeaders);
    var fileName = $"Semi_Verified_Players_{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}.xlsx";

    return new SemiVerifiedUsersFile(fileName, workbook);
  }

  private string ParseDate(DateTime? dateTime)
  {
      if (!dateTime.HasValue)
          return string.Empty;

      var parsedDate = dateTime.Value;
      return parsedDate.ToString("MMM d, yyyy, h:mm:ss tt");
  }
}