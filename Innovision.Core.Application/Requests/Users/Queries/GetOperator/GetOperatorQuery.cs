using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Accounts.Users.Operator.Queries.GetOperator;

public class GetOperatorQuery : IRequest<ApiResponse<OperatorListVm>>
{
    public Guid? CompanyId { get; set; }
    public int? BranchId { get; set; }
    public PagedQuery? PagedQuery { get; set; }
}

public class GetOperatorQueryHandler : IRequestHandler<GetOperatorQuery, ApiResponse<OperatorListVm>>
{
    private readonly IMapper _mapper;
    private readonly ICoreDbContext _dbContext;

    public GetOperatorQueryHandler(ICoreDbContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ApiResponse<OperatorListVm>> Handle(GetOperatorQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Accounts.Where(x => x.UserTypeId == Domain.Enums.UserTypes.Operator).AsQueryable();

        if (request.BranchId != null)
        {
            query = query.Where(q =>
                q.BranchId == request.BranchId);
        }

        var total = await query.CountAsync();

        if (request.PagedQuery != null)
            query = GetPagedQueryOperator(query, request.PagedQuery);

        var operatorList = await query
            .ProjectTo<OperatorListDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new ApiResponse<OperatorListVm>()
        {
            Data = new OperatorListVm
            {
                OperatorList = operatorList,
                Total = total,
                PageNumber = request.PagedQuery != null ? request.PagedQuery.PageNumber : 1,
                PageSize = request.PagedQuery != null ? request.PagedQuery.PageSize : operatorList.Count()
            }
        };
    }

    public IQueryable<Account> GetPagedQueryOperator(IQueryable<Account> query, PagedQuery pagedQuery)
    {
        if (!string.IsNullOrEmpty(pagedQuery.Search))
            query = query.Where(q => (q.FirstName + " " + q.LastName).ToLower().Contains(pagedQuery.Search.ToLower()));

        if (pagedQuery.PageNumber > 0)
            query = query.Skip((pagedQuery.PageNumber) * pagedQuery.PageSize);

        query = query.Take(pagedQuery.PageSize);

        return query;
    }
}