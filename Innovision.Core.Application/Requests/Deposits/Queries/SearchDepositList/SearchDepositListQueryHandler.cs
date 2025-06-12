using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Deposits.Queries.SearchDepositList;

public class SearchDepositListQueryHandler(ICoreDbContext coreDbContext, IMapper mapper, ICurrentUserService currentUserService) : IRequestHandler<SearchDepositListQuery, DepositVm>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<DepositVm> Handle(SearchDepositListQuery request, CancellationToken cancellationToken)
    {
        var depositQuery = _coreDbContext.Deposits
            .Include(o => o.AccountInfo)
            .Include(o => o.PaymentMethod)
            .Include(o => o.DepositStatus)
            .Where(m => m.PaymentMethodId != 2 || (m.Amount > 99 && m.PaymentMethodId == 2))
            .OrderByDescending(m => m.DepositId)
            .AsQueryable();

        if (request.BranchId.HasValue)
            depositQuery = depositQuery.Where(o => o.AccountInfo.BranchId == request.BranchId.Value);

        var totalCount = depositQuery.Count();

        depositQuery = GetPagedQuery(depositQuery, request);

        var deposits = await depositQuery
            .ProjectTo<DepositDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new DepositVm(deposits)
        {
            Total = totalCount,
            PageNumber = (request.PagedQuery != null) ? request.PagedQuery.PageNumber : 1,
            PageSize = (request.PagedQuery != null) ? request.PagedQuery.PageSize : deposits.Count()
        };
    }

    public IQueryable<Deposit> GetPagedQuery(IQueryable<Deposit> query, SearchDepositListQuery request)
    {
        if (request.DateFrom.HasValue && request.DateTo.HasValue)
            query = query.Where(q => q.CreatedOn >= request.DateFrom && q.CreatedOn <= request.DateTo);

        if (request.PagedQuery != null)
        {
            if (!string.IsNullOrEmpty(request.PagedQuery.Search))
            {
                query = query.Where(q => (q.AccountInfo.FirstName.ToLower() + " " + q.AccountInfo.LastName.ToLower()).Contains(request.PagedQuery.Search.ToLower())
                  || q.TransactionNo.ToLower().Contains(request.PagedQuery.Search.ToLower()));
                //need to add the amount
            }

            if (!request.downloadReport.HasValue)
            {
                if (request.PagedQuery.PageNumber > 0)
                    query = query.Skip((request.PagedQuery.PageNumber) * request.PagedQuery.PageSize);

                query = query.Take(request.PagedQuery.PageSize);
            }
        }

        return query;
    }
}