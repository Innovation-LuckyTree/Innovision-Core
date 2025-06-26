using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetUserUnusedItems;

public class GetUserUnusedItemsQueryHandler : IRequestHandler<GetUserUnusedItemsQuery, BetTransactionVm>
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

    public async Task<BetTransactionVm> Handle(GetUserUnusedItemsQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _mediator.Send(new GetCurrentAccountInfoQuery(), cancellationToken);

        var unusedOrderedItems = await _dbContext.BetTransactions
            .Include(o => o.Game)
            .Where(o => o.AccountInfoId == currentUser.AccountInfoId && o.GameId == request.GameId && !o.VoidTransaction)
            .ProjectTo<BetTransactionDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new BetTransactionVm(unusedOrderedItems);
    }
}