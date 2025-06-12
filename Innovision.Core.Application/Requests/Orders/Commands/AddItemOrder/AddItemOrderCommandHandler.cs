using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Notifications.MessageBrokers;
using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Orders.Commands.AddItemOrder;

public class AddItemOrderCommandHandler : IRequestHandler<AddItemOrderCommand, OrderItemVm>
{
    private readonly ICoreDbContext _dbContext;
    private readonly IMediator _mediator;
    private readonly IBackgroundCommandQueue _backgroundQueue;

    public AddItemOrderCommandHandler(ICoreDbContext dbContext, IMediator mediator, IBackgroundCommandQueue backgroundQueue)
    {
        _dbContext = dbContext;
        _mediator = mediator;
        _backgroundQueue = backgroundQueue;
    }

    public async Task<OrderItemVm> Handle(AddItemOrderCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await _mediator.Send(new GetCurrentAccountInfoQuery(), cancellationToken);

        var gameType = await _dbContext.GameTypes.Where(o => request.OrderItems.Select(e => e.GameTypeId).Contains(o.GameTypeId))
            .ToListAsync(cancellationToken);

        var orderedItems = request.OrderItems
            .Select(o => new OrderItem
            {
                AccountInfoId = currentUser.AccountInfoId,
                Values = o.Values,
                GameTypeId = o.GameTypeId,
                BetItemType = o.BetItemType,
                AmountBet = o.AmountBet,
                ExcessAmount = o.ExcessAmount,
                HasExcessAmount = o.ExcessAmount > 0,
                IsBonus = request.IsBonus.Value
            });

        if (request.TotalItems != orderedItems.Count())
            throw new Exception("Created Items is not equal to the requested items!");

        var order = new Order
        {
            GameId = request.GameId,
            AccountInfoId = currentUser.AccountInfoId,
            TotalAmount = orderedItems.Sum(o => o.AmountBet),
            TotalNoOfItems = orderedItems.Count(),
            OrderItems = orderedItems.ToList(),
            CommissionStatusId = Domain.Enums.CommissionStatus.Pending,
            IsBonus = request.IsBonus.Value
        };

        _dbContext.Orders.Add(order);

        await _dbContext.SaveChangesAsync(cancellationToken);

        _backgroundQueue.Enqueue(new AddOrderMigrationNotification(
            order.OrderId, request.GameId, gameType.First().GameTypeName, currentUser.AccountInfoId, order.TransactionNo, order.TotalAmount, order.TotalNoOfItems, order.CreatedOn, request.IsBonus ?? false));

        var orderItemIds = order.OrderItems.Select(o => o.OrderItemId);
        return new OrderItemVm(order.OrderId, order.TransactionNo, orderItemIds, order.TotalAmount);
    }
}
