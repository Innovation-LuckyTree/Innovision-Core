using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Withdrawals.Commands.ProcessWithdrawal;

public record ProcessWithdrawalCommand(long TransactionId, int Status) : IRequest<ApiResponse<WithdrawalDto>>;
