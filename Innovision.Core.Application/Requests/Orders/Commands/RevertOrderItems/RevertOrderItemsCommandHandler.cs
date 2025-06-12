using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;
using Innovision.Core.Application.Requests.Orders.Commands.ScheduleOrderItems;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Orders.Commands.RevertOrderItems;

public class RevertOrderItemsCommandHandler : IRequestHandler<ScheduleOrderItemsCommand, Unit>
{
    private readonly IMediator _mediator;
    private readonly ICoreDbContext _dbContext;

    public RevertOrderItemsCommandHandler(IMediator mediator, ICoreDbContext dbContext)
    {
        _mediator = mediator;
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(ScheduleOrderItemsCommand request, CancellationToken cancellationToken)
    {
        var currentAccount = await _mediator.Send(new GetCurrentAccountInfoQuery(), cancellationToken);

        foreach (var item in request.ScheduleOrderItems)
        {
            var itemOrders = await _dbContext.OrderItems
                .Where(o => item.OrderItems.Contains(o.OrderItemId) && !o.Used && !o.IsDeleted)
                .ToListAsync(cancellationToken);

            foreach (var itemOrder in itemOrders)
            {
                itemOrder.Used = false;
                itemOrder.UsedDate = null;
            }
        }

        return Unit.Value;
    }
}
