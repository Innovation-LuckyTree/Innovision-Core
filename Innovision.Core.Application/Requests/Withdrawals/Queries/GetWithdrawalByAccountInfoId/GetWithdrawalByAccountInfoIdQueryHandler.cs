using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Withdrawals.Queries.GetWithdrawalByAccountInfoId;

public class GetWithdrawalByAccountInfoIdQueryHandler : IRequestHandler<GetWithdrawalByAccountInfoIdQuery, ApiResponse<WithdrawalListVm>>
{
    private readonly IMapper _mapper;
    private readonly ICoreDbContext _dbContext;
    public GetWithdrawalByAccountInfoIdQueryHandler(ICoreDbContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    public async Task<ApiResponse<WithdrawalListVm>> Handle(GetWithdrawalByAccountInfoIdQuery request, CancellationToken cancellationToken)
    {

        var withdrawalQuery = _dbContext.Withdrawals
            .Include(o => o.AccountInfo)
            .Where(m => m.AccountInfoId == request.AccountInfoId)
            .OrderByDescending(m => m.CreatedOn)
            .AsQueryable();

        var totalCount = withdrawalQuery.Count();

        if (request.PagedQuery != null)
            withdrawalQuery = GetPagedQueryWithdrawal(withdrawalQuery, request.PagedQuery);


        var withdrawal = await withdrawalQuery
            .ProjectTo<WithdrawalDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new ApiResponse<WithdrawalListVm>()
        {
            Data = new WithdrawalListVm
            {
                WithdrawalList = withdrawal,
                Total = totalCount,
                PageNumber = (request.PagedQuery != null) ? request.PagedQuery.PageNumber : 1,
                PageSize = (request.PagedQuery != null) ? request.PagedQuery.PageSize : withdrawal.Count()
            }
        };

    }

    public IQueryable<Withdrawal> GetPagedQueryWithdrawal(IQueryable<Withdrawal> query, PagedQuery pagedQuery)
    {
        if (!string.IsNullOrEmpty(pagedQuery.Search))
        {
            query = query.Where(q => (q.AccountInfo.FirstName.ToLower() + " " + q.AccountInfo.LastName.ToLower()).Contains(pagedQuery.Search.ToLower())
              || q.TransactionNo.ToLower().Contains(pagedQuery.Search.ToLower()));
            //need to add the amount
        }

        if (pagedQuery.PageNumber > 0)
            query = query.Skip((pagedQuery.PageNumber) * pagedQuery.PageSize);

        query = query.Take(pagedQuery.PageSize);

        return query;
    }

}