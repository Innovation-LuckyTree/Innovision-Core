using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetOrderedByIdItems;

public class GetOrderedByIdItemsQueryHandler : IRequestHandler<GetOrderedByIdItemsQuery, OrderItemVm>
{
    private readonly ICoreDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public GetOrderedByIdItemsQueryHandler(ICoreDbContext dbContext, IMapper mapper, IMediator mediator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<OrderItemVm> Handle(GetOrderedByIdItemsQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _mediator.Send(new GetCurrentAccountInfoQuery(), cancellationToken);

        var orderedItems = await _dbContext.OrderItems
            .Include(o => o.Order)
            .Include(o => o.GameType)
            .Where(o => o.AccountInfoId == currentUser.AccountInfoId && o.OrderId == request.OrderId && !o.IsDeleted)
            .ProjectTo<OrderItemDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new OrderItemVm(orderedItems);
    }
}