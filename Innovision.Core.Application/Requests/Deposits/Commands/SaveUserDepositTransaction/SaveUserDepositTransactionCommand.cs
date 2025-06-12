using Innovision.Core.Application.Requests.Deposits.Queries;
using MediatR;

namespace Innovision.Core.Application.Requests.Deposits.Commands.AddUserDepositRequest;

public class SaveUserDepositTransactionCommand : IRequest<DepositDto>
{
    public decimal Amount { get; set; }
    public int PaymentMethodId { get; set; }
    public string TransactionType { get; set; }
    public string Remarks { get; set; }
}
