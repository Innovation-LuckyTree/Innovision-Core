using Innovision.Core.Application.Common.Contants;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Domain.Enums;
using Innovision.Core.Infrastructure.AccountServices.Models.Requests;
using Innovision.Core.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Orders.Commands.AddItemOrder;

public class DeleteOrdersCommandHandler(ICoreDbContext coreDbContext, IAccountServiceApi accountService, IMediator mediator) : IRequestHandler<DeleteOrdersCommand, Unit>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IAccountServiceApi _accountService = accountService;
    private readonly IMediator _mediator = mediator;

    public async Task<Unit> Handle(DeleteOrdersCommand request, CancellationToken cancellationToken)
    {
        var betTransaction = await _coreDbContext.BetTransactions.Where(o => o.BetTransactionId == request.BetTransactionId && !o.VoidTransaction)
            .FirstOrDefaultAsync(cancellationToken);

        if (betTransaction == null)
            return Unit.Value;

        betTransaction.VoidTransaction = true;

        await _coreDbContext.SaveChangesAsync(cancellationToken);

        await ProcessRefund(betTransaction, cancellationToken);
    
        return Unit.Value;
    }

    private async Task ProcessRefund(BetTransaction betTransaction, CancellationToken cancellationToken)
    {
        var currentAccount = await _mediator.Send(new GetCurrentAccountInfoQuery(), cancellationToken);

        var accountTransaction = new AccountTransactionRequest()
        {
            AccountId = currentAccount.AccountCreditId,
            Amount = betTransaction.AmountBet,
            ModeOfTransaction = "App Refund",
            TransactionNo = betTransaction.BetTransactionId.ToString(),
            TransactionReference = betTransaction.BetTransactionId.ToString(),
            Notes = "Order Refund",
            AccountType = WalletAccountTypes.GetWalletAccountType(UserTypes.Player)
        };

        await _accountService.AddWalletAccount(accountTransaction, cancellationToken);
    }
}