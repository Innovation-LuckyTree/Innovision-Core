using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Application.Common.Models;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Notifications.MessageBrokers;
using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Orders.Commands.ScheduleBetTransactions;

public class ScheduleBetTransactionsCommandHandler(IMediator mediator, ICoreDbContext dbContext, IBackgroundCommandQueue backgroundQueue) : IRequestHandler<ScheduleBetTransactionsCommand, Unit>
{
    private readonly IMediator _mediator = mediator;
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IBackgroundCommandQueue _backgroundQueue = backgroundQueue;

    public async Task<Unit> Handle(ScheduleBetTransactionsCommand request, CancellationToken cancellationToken)
    {
        var betInformations = new List<BetInformation>();

        var currentAccount = await _mediator.Send(new GetCurrentAccountInfoQuery(), cancellationToken);

        foreach (var item in request.ScheduleBetTransactions)
        {
            var itemOrders = await _dbContext.BetTransactions
                .Where(o => item.BetTransactions.Contains(o.BetTransactionId) && !o.VoidTransaction)
                .ToListAsync(cancellationToken);

            foreach (var itemOrder in itemOrders)
            {
                // itemOrder.Used = true;
                // itemOrder.UsedDate = DateTime.Now;
                itemOrder.LastModified = DateTime.Now;
            }

            _dbContext.BetTransactions.UpdateRange(itemOrders);

            betInformations.AddRange(itemOrders.Select(o => new BetInformation
            {
                GameScheduleId = item.GameScheduleId,
                Value = o.BetValue,
                Amount = o.AmountBet
            }));
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        _backgroundQueue.Enqueue(new LogCombinationLimitNotification(betInformations));
        _backgroundQueue.Enqueue(new AddBetTransactionNotification(request.ScheduleBetTransactions.SelectMany(o => o.BetTransactions)));

        return Unit.Value;
    }
}
