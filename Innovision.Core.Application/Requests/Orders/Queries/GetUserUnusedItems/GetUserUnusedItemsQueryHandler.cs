using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetUserUnusedItems;

public class GetUserUnusedItemsQueryHandler : IRequestHandler<GetUserUnusedItemsQuery, OrderItemVm>
{
    private readonly ICoreDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public GetUserUnusedItemsQueryHandler(ICoreDbContext dbContext, IMapper mapper, IMediator mediator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<OrderItemVm> Handle(GetUserUnusedItemsQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _mediator.Send(new GetCurrentAccountInfoQuery(), cancellationToken);

        var unusedOrderedItems = await _dbContext.OrderItems
            .Include(o => o.Order)
            .Where(o => o.AccountInfoId == currentUser.AccountInfoId && o.Order.GameId == request.GameId && !o.Used && !o.IsDeleted)
            .ProjectTo<OrderItemDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new OrderItemVm(unusedOrderedItems);
    }
}