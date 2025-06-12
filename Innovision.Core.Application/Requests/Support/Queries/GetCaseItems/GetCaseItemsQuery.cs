using Innovision.Core.Common.Models;
using Innovision.Core.Infrastructure.Support.Models.Response;
using MediatR;

namespace Innovision.Core.Application.Requests.Support.Queries.GetCaseItems;
public record GetCaseItemsQuery(SupportPagedQuery PagedQuery, DateTime StartDate, DateTime EndDate, int OrganizationId) : IRequest<GetCasesResponse>;
