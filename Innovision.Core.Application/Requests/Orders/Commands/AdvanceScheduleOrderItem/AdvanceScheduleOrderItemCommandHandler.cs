using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Orders.Commands.AdvanceUseOrderedItem;
using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Commands.AdvanceScheduleOrderItem;

public class AdvanceScheduleOrderItemCommandHandler : IRequestHandler<AdvanceScheduleOrderItemCommand, Unit>
{
    private readonly IMediator _mediator;
    private readonly ICoreDbContext _dbContext;

    public AdvanceScheduleOrderItemCommandHandler(IMediator mediator, ICoreDbContext dbContext)
    {
        _mediator = mediator;
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(AdvanceScheduleOrderItemCommand request, CancellationToken cancellationToken)
    {
        TimeSpan gameTime;

        if (!TimeSpan.TryParse(request.GameTime, out gameTime))
        {
            throw new Exception("Unable to identify draw time!");
        }

        var requestList = request.ScheduleOrderItems.Select(o => _mediator.Send(new AdvanceUseOrderedItemCommand(request.Date, gameTime, o.OrderItems), cancellationToken));

        await Task.WhenAll(requestList);

        return Unit.Value;
    }
}
