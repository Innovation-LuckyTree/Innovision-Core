using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Orders.Queries.GetOrderItemsList;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetOrderItemDetail;

public class GetOrderItemDetailQueryHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<GetOrderItemDetailQuery, OrderItemDetailDto>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<OrderItemDetailDto> Handle(GetOrderItemDetailQuery request, CancellationToken cancellationToken)
    {
        var orderItem = await _dbContext.OrderItems.Where(e => e.OrderItemId == request.OrderItemId && !e.IsDeleted)
            .ProjectTo<OrderItemDetailDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return orderItem;
    }
}