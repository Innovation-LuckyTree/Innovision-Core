using Innovision.Core.Application.Requests.Withdrawals.Queries.GetWithdrawal;
using MediatR;
using ReportServices.Infrastructure.Helpers;

namespace Innovision.Core.Application.Requests.Withdrawals.Queries.GetWithdrawalExport;

public class GetWithdrawalExportQueryHandler(IMediator mediator) : IRequestHandler<GetWithdrawalExportQuery, WithdrawalFile>
{
  private readonly IMediator _mediator = mediator;

  public async Task<WithdrawalFile> Handle(GetWithdrawalExportQuery request, CancellationToken cancellationToken)
  {
    // reuse the GetSemiVerifiedUsersQuery handler to get data
    var query = new GetWithdrawalQuery{
        BranchId= request.BranchId,
        DateFrom= request.DateFrom,
        DateTo= request.DateTo,
        PagedQuery=null
    };
    var vm = await _mediator.Send(query, cancellationToken);

    var columnHeaders = new Dictionary<string, string>
        {
            { "TransactionNo", "Reference Number" },
            { "Name", "Display Name" },
            { "Amount", "Amount" },
            { "StatusDisplay", "Status" },
            { "PaymentMethod", "Payment Method" },
            { "TransactionDate", "Date" }
        };

    var exportData = vm.Data.WithdrawalList.Select(withdrawal => new
    {
      TransactionNo = withdrawal.TransactionNo,
      Name = withdrawal.Name,
      Amount = withdrawal.Amount,
      StatusDisplay = withdrawal.StatusDisplay,
      PaymentMethod = withdrawal.PaymentMethod,
      TransactionDate = ParseDate(withdrawal.TransactionDate.DateTime),
    }).ToList();

    var workbook = ClosedXmlExcelBuilder.ExportExcelToBase64(exportData, columnHeaders);
    var fileName = $"Withdrawal_{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}.xlsx";

    return new WithdrawalFile(fileName, workbook);
  }

  private string ParseDate(DateTime? dateTime)
  {
      if (!dateTime.HasValue)
          return string.Empty;

      var parsedDate = dateTime.Value;
      return parsedDate.ToString("MMM d, yyyy, h:mm:ss tt");
  }
}