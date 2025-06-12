using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Withdrawals.Commands.AddWithdrawalByUsers;

public class UpdateWithdrawalStatusCommand : IRequest<ApiResponse<WithdrawalDto>>
{
    public long TransactionId { get; set; }
    public int Status { get; set; }
    public string? ImageProof { get; set; }
    public string? Remarks { get; set; }
}
