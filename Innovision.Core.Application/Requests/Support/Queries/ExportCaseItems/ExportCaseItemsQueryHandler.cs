using Innovision.Core.Infrastructure.Interfaces;
using Innovision.Core.Infrastructure.Support.Models.Response;
using MediatR;
using ReportServices.Infrastructure.Helpers;

namespace Innovision.Core.Application.Requests.Support.Queries.ExportCaseItems;

public class ExportCaseItemsQueryHandler : IRequestHandler<ExportCaseItemsQuery, CaseItemsFile>
{
    private readonly ISupportApi _supportApi;

    public ExportCaseItemsQueryHandler(ISupportApi supportApi)
    {
        _supportApi = supportApi;
    }

    public async Task<CaseItemsFile> Handle(ExportCaseItemsQuery request, CancellationToken cancellationToken)
    {
        var cases = await _supportApi.GetCases(new GetCasesRequest
        {
            OrganizationId = request.OrganizationId,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        }, cancellationToken);
        
        var columnHeaders = new Dictionary<string, string>
            {
                { "CaseId", "Reference Number" },
                { "FullName", "Customer Name" },
                { "Email", "Customer Email" },
                { "FeedbackType", "Feedback Type" },
                { "FeedbackParticulars", "Feedback Particulars" },
                { "Date", "Date & Time Received" },
                { "Actions", "Actions Taken" },
                { "NoOfHours", "No. of Hours From Receipt to Actions Taken" },
                { "Status", "Status" }
            };
            
        var exportData = cases.Cases.Select(account => new
        {
            CaseId = account.CaseId,
            FullName = account.Fullname,
            Email = account.CaseOwner.Email,
            FeedbackType = "",
            FeedbackParticulars = account.Description,
            Date = account.CreatedOn.ToString("MMM d, yyyy, HH:MM"),
            Actions = account.Comments.Aggregate("", (current, comment) => current + $"({comment.CreatedOn:MMM d yyyy, HH:mm}) {comment.Comment}" + "\n"),
            NoOfHours = account.NoOfHours,
            Status = account.Status
        }).ToList();



        var workbook = ClosedXmlExcelBuilder.ExportExcelToBase64(exportData, columnHeaders);
        var fileName = $"Case_Items_{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}.xlsx";

        return new CaseItemsFile(fileName, workbook);
    }
}