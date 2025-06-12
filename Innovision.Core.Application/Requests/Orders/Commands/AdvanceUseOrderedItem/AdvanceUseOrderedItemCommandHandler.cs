using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Orders.Commands.AdvanceUseOrderedItem;

public class AdvanceUseOrderedItemCommandHandler : IRequestHandler<AdvanceUseOrderedItemCommand, Unit>
{
    private readonly ICoreDbContext _dbContext;
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;


    public AdvanceUseOrderedItemCommandHandler(ICoreDbContext dbContext, IMediator mediator, ICurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(AdvanceUseOrderedItemCommand request, CancellationToken cancellationToken)
    {
        var dateNow = DateTime.UtcNow;

        var itemOrders = await _dbContext.OrderItems
            .Where(o => request.OrderItems.Contains(o.OrderItemId) && !o.Used && !o.IsDeleted)
            .ToListAsync(cancellationToken);

        // TODO : GET game schedule thru game api
        // var gameSchedules = await _dbContext.GameSchedules
        //     .Include(o => o.GameDrawType)
        //     .Where(o => o.Date == request.Date && o.GameDrawType.DrawSchedule == request.DrawTime)
        //     .ToListAsync(cancellationToken);

        foreach(var item in itemOrders)
        {
            item.Used = true;
            item.UsedDate = DateTime.UtcNow;
        }

        //TODO: 
        // create bet items on each item orders
        // create bet transction that will wrap all bet items
        // get the result of the bet transaction and update bet orders;

        // var betTransactions = itemOrders.GroupBy(gameType => gameType.GameTypeId)
        //     .Select(o => CreateBetTransaction(
        //         gameSchedules.Where(g => g.GameTypeId == o.Max(e))
        //             .FirstOrDefault()
        //             .GameScheduleId, o));

        // foreach(var transaction in betTransactions)
        // {
        //     var result = await _mediator.Send(transaction, cancellationToken);
        // }

        return Unit.Value;
    }

    // public CreateBetTransactionCommand CreateBetTransaction(long gameScheduleId, IEnumerable<OrderItem> orderItems)
    // {
    //     var command = new CreateBetTransactionCommand
    //     {
    //         AccountInfoId = Guid.Parse(_currentUserService.UserId),
    //         NoOfBets = orderItems.Count(),
    //         GameScheduleId = gameScheduleId,
    //         OrderItemIds = orderItems.Select(o => o.OrderItemId),
    //         PricePerBet = orderItems.FirstOrDefault().PricePerItem,
    //         TotalAmount = orderItems.Sum(o => o.PricePerItem),
    //         BetType = (int)BetTypes.Advance
    //     };

    //     return command;
    // }
}