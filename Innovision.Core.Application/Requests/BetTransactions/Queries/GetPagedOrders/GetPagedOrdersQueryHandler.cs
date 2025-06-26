using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetPagedOrders;

public class GetPagedOrdersQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetPagedOrdersQuery, OrdersVm>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<OrdersVm> Handle(GetPagedOrdersQuery request, CancellationToken cancellationToken)
    {
        var query = GetOrderQuery(request);

        var totalOrders = await query.CountAsync(cancellationToken);

        var orders = await query
            .OrderBy(o => o.BetTransactionId)
            .Skip(request.Start)
            .Take(request.Size)
            .ProjectTo<OrdersDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new OrdersVm(orders, totalOrders);
    }

    public IQueryable<BetTransaction> GetOrderQuery(GetPagedOrdersQuery request)
    {
        var orders = _coreDbContext.BetTransactions
            .Where(x => !x.VoidTransaction)
            .AsQueryable();

        if (request.StartOrderId.HasValue)
            orders = orders.Where(x => x.BetTransactionId >= request.StartOrderId);

        if (request.StartDate.HasValue)
            orders = orders.Where(x => x.CreatedOn.Date >= request.StartDate.Value.Date);

        if (request.EndDate.HasValue)
            orders = orders.Where(x => x.CreatedOn.Date <= request.EndDate.Value.Date);

        return orders;
    }
}
