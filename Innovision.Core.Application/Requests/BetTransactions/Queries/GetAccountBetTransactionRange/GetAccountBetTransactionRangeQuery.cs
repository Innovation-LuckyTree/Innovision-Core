using Innovision.Core.Application.Common;
using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.BetTransactions.Queries.GetAccountBetTransactionRange;

public record GetAccountBetTransactionRangeQuery(DateTime CreatedDateFrom, DateTime CreatedDateTo, DateTime? ModifiedDateFrom, DateTime? ModifiedDateTo, PagedQuery? PagedQuery) : IRequest<ApiResponse<BetTransactionVm>>;

