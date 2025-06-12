using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Orders.Queries.GetOrderItemsList;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetOrderItemsByIds;

public class GetOrderItemsByIdsQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetOrderItemsByIdsQuery, OrderItemDetailsVm>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<OrderItemDetailsVm> Handle(GetOrderItemsByIdsQuery request, CancellationToken cancellationToken)
    {
        var orderItems = await _coreDbContext.OrderItems.Where(e => request.OrderItemIds.Contains(e.OrderItemId) && !e.IsDeleted)
            .ProjectTo<OrderItemDetailDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new OrderItemDetailsVm(orderItems);
    }
}
