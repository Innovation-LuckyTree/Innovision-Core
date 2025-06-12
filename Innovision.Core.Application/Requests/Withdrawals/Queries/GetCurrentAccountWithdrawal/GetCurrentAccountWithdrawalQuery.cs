using Innovision.Core.Application.Common;
using Innovision.Core.Application.Requests.Withdrawals.GetCurrentAccountWithdrawal;
using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.Withdrawals.Queries.GetCurrentAccountWithdrawal;

public record GetCurrentAccountWithdrawalQuery(int? Status, PagedQuery PagedQuery) : IRequest<ApiResponse<WithdrawalInfoVm>>;
