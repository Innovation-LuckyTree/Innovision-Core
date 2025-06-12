using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Application.Common.Models;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Notifications.MessageBrokers;
using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Orders.Commands.ScheduleOrderItems;

public class ScheduleOrderItemsCommandHandler(IMediator mediator, ICoreDbContext dbContext, IBackgroundCommandQueue backgroundQueue) : IRequestHandler<ScheduleOrderItemsCommand, Unit>
{
    private readonly IMediator _mediator = mediator;
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IBackgroundCommandQueue _backgroundQueue = backgroundQueue;

    public async Task<Unit> Handle(ScheduleOrderItemsCommand request, CancellationToken cancellationToken)
    {
        var betInformations = new List<BetInformation>();

        var currentAccount = await _mediator.Send(new GetCurrentAccountInfoQuery(), cancellationToken);

        foreach (var item in request.ScheduleOrderItems)
        {
            var itemOrders = await _dbContext.OrderItems
                .Where(o => item.OrderItems.Contains(o.OrderItemId) && !o.Used && !o.IsDeleted)
                .ToListAsync(cancellationToken);

            foreach (var itemOrder in itemOrders)
            {
                itemOrder.Used = true;
                itemOrder.UsedDate = DateTime.UtcNow;
                itemOrder.LastModified = DateTime.UtcNow;
            }

            _dbContext.OrderItems.UpdateRange(itemOrders);

            betInformations.AddRange(itemOrders.Select(o => new BetInformation
            {
                GameScheduleId = item.GameScheduleId,
                Value = o.Values,
                Amount = o.AmountBet
            }));
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        _backgroundQueue.Enqueue(new LogCombinationLimitNotification(betInformations));
        _backgroundQueue.Enqueue(new AddBetTransactionNotification(request.ScheduleOrderItems.SelectMany(o => o.OrderItems)));

        return Unit.Value;
    }
}
