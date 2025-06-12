using Innovision.Core.Application.Common;
using Innovision.Core.Application.Requests.Withdrawals.Queries.GetWithdrawalByAccountInfoId;
using MediatR;

namespace Innovision.Core.Application.Requests.Withdrawals.Queries.GetWithdrawalByTransactionNo;

public record GetWithdrawalByTransactionNoQuery(string transactionNo) : IRequest<ApiResponse<WithdrawalDto>> { }
