using Innovision.Core.Application.Requests.Deposits.Queries.SearchDepositList;
using MediatR;
using ReportServices.Infrastructure.Helpers;

namespace Innovision.Core.Application.Requests.Deposits.Queries.SearchDepositListExport;

public class SearchDepositListExportQueryHandler(IMediator mediator) : IRequestHandler<SearchDepositListExportQuery, DepositListFile>
{
  private readonly IMediator _mediator = mediator;

  public async Task<DepositListFile> Handle(SearchDepositListExportQuery request, CancellationToken cancellationToken)
  {
    // reuse the SearchDepositListQuery handler to get data
    var vm = await _mediator.Send(new SearchDepositListQuery
    {
      BranchId = request.BranchId,
      DateFrom = request.DateFrom,
      DateTo = request.DateTo,
      PagedQuery = null // pass null to get all data
    }, cancellationToken);

    var columnHeaders = new Dictionary<string, string>
        {
            { "RefNumber", "Reference Number" },
            { "Name", "Display Name" },
            { "Amount", "Amount" },
            { "Status", "Status" },
            { "PaymentMethod", "Payment Method" },
            { "CreatedOn", "Date" }
        };

    var exportData = vm.Deposits.Select(account => new
    {
      RefNumber = account.TransactionNo,
      Name = account.FullName,
      Amount = account.Amount,
      Status = account.DepositStatus,
      PaymentMethod = account.PaymentMethod,
      CreatedOn = ParseDate(account.TransactionDate.Value.Date)
    }).ToList();

    var workbook = ClosedXmlExcelBuilder.ExportExcelToBase64(exportData, columnHeaders);
    var fileName = $"Deposits_{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}.xlsx";

    return new DepositListFile(fileName, workbook);
  }

  private string ParseDate(DateTime? dateTime)
  {
      if (!dateTime.HasValue)
          return string.Empty;

      var parsedDate = dateTime.Value;
      return parsedDate.ToString("MMM d, yyyy, h:mm:ss tt");
  }
}