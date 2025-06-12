using MediatR;

namespace Innovision.Core.Application.Requests.Support.Queries.ExportCaseItems;
public record ExportCaseItemsQuery(DateTime StartDate, DateTime EndDate, int OrganizationId) : IRequest<CaseItemsFile>;
