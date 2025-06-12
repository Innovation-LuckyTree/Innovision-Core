using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetOrderDetail;

public class GetOrderDetailQueryHandler(ICoreDbContext coreDbContext, IMapper mapper, IMediator mediator) : IRequestHandler<GetOrderDetailQuery, OrdersDto>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;
    private readonly IMediator _mediator = mediator;

    public async Task<OrdersDto> Handle(GetOrderDetailQuery request, CancellationToken cancellationToken)
    {
        var account = await _mediator.Send(new GetCurrentAccountInfoQuery(), cancellationToken);

        var order = await _coreDbContext.Orders
            .Where(x => x.OrderId == request.OrderId)
            .ProjectTo<OrdersDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (account != null && account.AccountInfoId != (order?.PlayerAccountId ?? 0))
            return null;

        return order;
    }
}
