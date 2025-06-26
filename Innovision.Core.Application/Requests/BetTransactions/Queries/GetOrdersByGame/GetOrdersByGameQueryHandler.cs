using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;
using Innovision.Core.Application.Requests.Orders.Queries.GetOrders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetOrdersByGame;

public class GetOrdersByGameQueryHandler(ICoreDbContext dbContext, IMapper mapper, IMediator mediator) : IRequestHandler<GetOrdersByGameQuery, OrdersVm>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly IMediator _mediator = mediator;

    public async Task<OrdersVm> Handle(GetOrdersByGameQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _mediator.Send(new GetCurrentAccountInfoQuery(), cancellationToken);

        var orderedItems = await _dbContext.BetTransactions
            .Where(o => o.AccountInfoId == currentUser.AccountInfoId && o.GameId == request.GameId && !o.VoidTransaction)
            .ProjectTo<OrdersDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new OrdersVm(orderedItems);

    }
}