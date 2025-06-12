using Innovision.Core.Application.Requests.Deposits.Queries;
using MediatR;

namespace Innovision.Core.Application.Requests.Deposits.Commands.AddUserDepositRequest;

public class AddUserDepositRequestCommand : IRequest<DepositDto>
{
    public long AccountInfoId { get; set; }
    public decimal Amount { get; set; }
    public int PaymentMethod { get; set; }
    public int Status { get; set; }
    public string TransactionType { get; set; }
}
