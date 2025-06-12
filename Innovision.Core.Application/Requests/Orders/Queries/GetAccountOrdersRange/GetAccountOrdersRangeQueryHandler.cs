using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Orders.Queries.GetOrderDetail;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetAccountOrdersRange;

public class GetAccountOrdersRangeQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetAccountOrdersRangeQuery, ApiResponse<GetAccountOrdersVm>>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiResponse<GetAccountOrdersVm>> Handle(GetAccountOrdersRangeQuery request, CancellationToken cancellationToken)
    {

        var createdDateTo = request.CreatedDateTo.Date.AddDays(1).AddTicks(-1);
        var modifiedDateTo = request.ModifiedDateTo?.Date.AddDays(1).AddTicks(-1);

        var orders = _coreDbContext.Orders
            .Where(x => !x.IsDeleted
                && x.CreatedOn >= request.CreatedDateFrom && x.CreatedOn <= createdDateTo
                || (x.LastModified >= request.ModifiedDateFrom && x.LastModified <= modifiedDateTo))
            .AsQueryable();

        if (request.PagedQuery != null)
            orders = FilterQuery(orders, request.PagedQuery);

        var orderlist = await orders
                .ProjectTo<OrdersDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

        return new ApiResponse<GetAccountOrdersVm>()
            {
                Data = new GetAccountOrdersVm
                {
                    Orders = orderlist,
                    Total = orderlist?.Count() ?? 0,
                    PageNumber = request.PagedQuery != null ? request.PagedQuery.PageNumber : 1,
                    PageSize = request.PagedQuery != null ? request.PagedQuery.PageSize : orderlist.Count()
                }
            };
    }

    public IQueryable<Order> FilterQuery(IQueryable<Order> query, PagedQuery pagedQuery)
    {
        if (pagedQuery.PageNumber > 0)
            query = query.Skip(pagedQuery.PageNumber * pagedQuery.PageSize);

        query = query.Take(pagedQuery.PageSize);

        return query;
    }
}
