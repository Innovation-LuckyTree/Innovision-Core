using Innovision.Core.Application.Common;
using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetAccountOrdersRange;

public record GetAccountOrdersRangeQuery(DateTime CreatedDateFrom, DateTime CreatedDateTo, DateTime? ModifiedDateFrom, DateTime? ModifiedDateTo, PagedQuery? PagedQuery) : IRequest<ApiResponse<GetAccountOrdersVm>>;

