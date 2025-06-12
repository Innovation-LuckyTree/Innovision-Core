using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetOrderItemsList;

public class GetOrderItemsListQueryHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<GetOrderItemsListQuery, OrderItemDetailVm>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<OrderItemDetailVm> Handle(GetOrderItemsListQuery request, CancellationToken cancellationToken)
    {
        var orderItemList = await _dbContext.OrderItems.Where(e => e.OrderItemId > request.OrderItemId && !e.IsDeleted)
            .Include(e => e.AccountInfo)
                .ThenInclude(s => s.Branch)
            .Include(e => e.GameType)
                .ThenInclude(g => g.Game)
            .OrderBy(e => e.OrderItemId)
            .ProjectTo<OrderItemDetailDto>(_mapper.ConfigurationProvider)
            .Take(request.Size)
            .ToListAsync(cancellationToken);
        
        var lastItemId = orderItemList.Select(o => o.OrderItemId).OrderByDescending(o => o).FirstOrDefault();

        return new OrderItemDetailVm(orderItemList, lastItemId);
    }
}