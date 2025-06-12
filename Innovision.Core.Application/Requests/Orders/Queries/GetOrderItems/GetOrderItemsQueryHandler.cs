using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetOrderItems;

public class GetOrderItemsQueryHandler : IRequestHandler<GetOrderItemsQuery, OrderItemVm>
{
    private readonly ICoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetOrderItemsQueryHandler(ICoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<OrderItemVm> Handle(GetOrderItemsQuery request, CancellationToken cancellationToken)
    {
        var orderItems = await _dbContext.OrderItems.Where(o => request.OrderItemIds.Contains(o.OrderItemId) && !o.IsDeleted)
            .ProjectTo<OrderItemDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new OrderItemVm(orderItems);
    }
}
