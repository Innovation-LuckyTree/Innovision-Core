using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Application.Common.Models;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Notifications.MessageBrokers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Orders.Commands.UseOrderedItem;

public class UseOrderedItemCommandHandler(ICoreDbContext dbContext, IBackgroundCommandQueue backgroundQueue) : IRequestHandler<UseOrderedItemCommand, Unit>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IBackgroundCommandQueue _backgroundQueue = backgroundQueue;

    public async Task<Unit> Handle(UseOrderedItemCommand request, CancellationToken cancellationToken)
    {
        var dateNow = DateTime.UtcNow;

        var itemOrders = await _dbContext.OrderItems
            .Where(o => request.OrderItems.Contains(o.OrderItemId) && !o.Used && !o.IsDeleted)
            .ToListAsync(cancellationToken);

        foreach (var item in itemOrders)
        {
            item.Used = true;
            item.UsedDate = DateTime.UtcNow;
        }

        var betInformations = itemOrders.Select(o => new BetInformation
        {
            GameScheduleId = request.GameScheduleId,
            Value = o.Values,
            Amount = o.AmountBet
        });

        _backgroundQueue.Enqueue(new LogCombinationLimitNotification(betInformations));
        _backgroundQueue.Enqueue(new AddBetTransactionNotification(request.OrderItems));

        return Unit.Value;
    }
}