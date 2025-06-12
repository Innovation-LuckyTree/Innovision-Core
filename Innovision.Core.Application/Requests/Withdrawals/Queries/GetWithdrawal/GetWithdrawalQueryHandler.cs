using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Withdrawals.Queries.GetWithdrawal;

public class GetWithdrawalQueryHandler : IRequestHandler<GetWithdrawalQuery, ApiResponse<WithdrawalListVm>>
{
    private readonly IMapper _mapper;
    private readonly ICoreDbContext _dbContext;
    private readonly ICurrentUserService _currentUserService;

    public GetWithdrawalQueryHandler(ICoreDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _currentUserService = currentUserService;
    }
    public async Task<ApiResponse<WithdrawalListVm>> Handle(GetWithdrawalQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var withdrawalQuery = _dbContext.Withdrawals
                .Include(m => m.AccountInfo)
                .Include(m => m.BankReference).OrderByDescending(m => m.TransactionId).AsQueryable();

            withdrawalQuery = GetWithdrawaFilterQuery(withdrawalQuery, request);

            var totalCount = await withdrawalQuery.CountAsync(cancellationToken);

            withdrawalQuery = GetPagedQueryWithdrawal(withdrawalQuery, request);

            var withdrawal = await withdrawalQuery
                 .ProjectTo<WithdrawalDto>(_mapper.ConfigurationProvider)
                 .OrderByDescending(x => x.TransactionId)
                 .ToListAsync(cancellationToken);

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
        catch (Exception ex)
        {
            throw;
        }
    }
    public IQueryable<Withdrawal> GetWithdrawaFilterQuery(IQueryable<Withdrawal> query, GetWithdrawalQuery request)
    {

        if (request.BranchId.HasValue)
            query = query.Where(o => o.AccountInfo.BranchId == request.BranchId.Value);
        if (request.DateFrom.HasValue && request.DateTo.HasValue)
            query = query.Where(q => q.CreatedOn >= request.DateFrom && q.CreatedOn <= request.DateTo.Value.Date.AddDays(1).AddTicks(-1));
        if (!string.IsNullOrEmpty(request.PagedQuery?.Search))
        {
            query = query.Where(q => (q.AccountInfo.FirstName.ToLower() + " " + q.AccountInfo.LastName.ToLower()).Contains(request.PagedQuery.Search.ToLower())
              || q.TransactionNo.ToLower().Contains(request.PagedQuery.Search.ToLower()));
            //need to add the amount
        }

        return query;
    }
    public IQueryable<Withdrawal> GetPagedQueryWithdrawal(IQueryable<Withdrawal> query, GetWithdrawalQuery request)
    {
        if (request.PagedQuery != null)
        {
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