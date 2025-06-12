using Innovision.Core.Application.Requests.Withdrawals.Commands.WithdrawBalance;
using Innovision.Core.Infrastructure.AccountServices.Models.Requests;
using Innovision.Core.Infrastructure.Interfaces;
using MediatR;

namespace Innovision.Core.Application.Requests.Withdrawals.Commands.WithdrawBalance
{
    public class WithdrawBalanceCommand : IRequest<object>
    {
        public Guid AccountId { get; set; }
        public string TransactionNo { get; set; }
        public decimal Amount { get; set; }
        public string Notes { get; set; }
        public string ModeOfTransaction { get; set; }
    }
}
public class WithdrawBalanceCommandHandler : IRequestHandler<WithdrawBalanceCommand, object>
{
    private readonly IAccountServiceApi _accountServiceApi;

    public WithdrawBalanceCommandHandler(IAccountServiceApi accountServiceApi)
    {
        _accountServiceApi = accountServiceApi;
    }

    public async Task<object> Handle(WithdrawBalanceCommand request, CancellationToken cancellationToken)
    {
        var result = await _accountServiceApi.WithdrawBalance(new WithdrawBalanceRequest
        {
            AccountId = request.AccountId,
            Amount = request.Amount,
            ModeOfTransaction = request.ModeOfTransaction,
            TransactionNo = request.TransactionNo,
            Notes = request.Notes
        }, cancellationToken);

        return result;
    }
}
