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
        var order = await _coreDbContext.Orders.Where(o => o.OrderId == request.OrderId && !o.IsDeleted)
            .Include(e => e.OrderItems)
            .FirstOrDefaultAsync(cancellationToken);

        if (order == null)
            return Unit.Value;

        order.IsDeleted = true;

        order.OrderItems = order.OrderItems.Select(o =>
        {
            o.IsDeleted = true;

            return o;
        }).ToList();

        await _coreDbContext.SaveChangesAsync(cancellationToken);
        await ProcessRefund(order, cancellationToken);
    
        return Unit.Value;
    }

    private async Task ProcessRefund(Order order, CancellationToken cancellationToken)
    {
        var currentAccount = await _mediator.Send(new GetCurrentAccountInfoQuery(), cancellationToken);

        var accountTransaction = new AccountTransactionRequest()
        {
            AccountId = currentAccount.AccountCreditId,
            Amount = order.TotalAmount,
            ModeOfTransaction = "App Refund",
            TransactionNo = order.TransactionNo,
            TransactionReference = order.TransactionNo,
            Notes = "Order Refund",
            AccountType = WalletAccountTypes.GetWalletAccountType(UserTypes.Player)
        };

        await _accountService.AddWalletAccount(accountTransaction, cancellationToken);
    }
}