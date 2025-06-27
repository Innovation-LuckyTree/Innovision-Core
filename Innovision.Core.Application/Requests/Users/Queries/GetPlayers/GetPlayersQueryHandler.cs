using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Users.AgentAndPlayer.Queries.GetDownlinePlayers;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Infrastructure.Interfaces;
using Innovision.Core.Infrastructure.PaymentServices.Models.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Accounts.Users.AgentAndPlayer.Queries.GetPlayers;

public class GetPlayersQueryHandler : IRequestHandler<GetPlayersQuery, ApiResponse<DownlinePlayersVm>>
{
    private readonly IMapper _mapper;
    private readonly ICoreDbContext _dbContext;
    private readonly IPaymentServicesApi _paymentServicesApi;

    public GetPlayersQueryHandler(ICoreDbContext dbContext, IMapper mapper, IPaymentServicesApi paymentServicesApi)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _paymentServicesApi = paymentServicesApi;
    }

    public async Task<ApiResponse<DownlinePlayersVm>> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Accounts
            .Include(m => m.Branch)
            .Where(x => x.UserTypeId == Domain.Enums.UserTypes.Player)
            .OrderByDescending(m => m.CreatedOn).AsQueryable();

        query = GetFilteredQueryOperator(query, request);

        var total = await query.CountAsync(cancellationToken);

        if(request.PagedQuery != null)
            query = GetPagedQueryOperator(query, request, request.downloadReport);


        var listData = await query
            .ProjectTo<DownlinePlayersDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        // Get all players
        var uplines = await _dbContext.Accounts.Where(m => listData.Select(m => m.RefferalCode).Contains(m.RefferralKey))
            .ToListAsync();

        var guidList = listData.Select(m => m.AccountCreditId).ToList();
        var userBalances = await _paymentServicesApi.GetAccountBalancesRequest(new GetAccountBalanceRequest(guidList), cancellationToken);

        foreach (var item in listData)
        {
            var upline = uplines.Where(m => m.RefferralKey == item.RefferalCode).FirstOrDefault();
            item.RecruiterName = (upline != null) ? $"{upline.FirstName} {upline.LastName}" : "";

            var userBalance = userBalances.Where(m => m.AccountId == item.AccountCreditId).FirstOrDefault();
            item.CreditBalance = userBalance?.Balance;
            item.CreditUpdatedOn = userBalance?.DateUpdated;
        }

        return new ApiResponse<DownlinePlayersVm>()
        {
            Data = new DownlinePlayersVm
            {
                Results = listData,
                Total = total,
                PageNumber = request.PagedQuery != null ? request.PagedQuery.PageNumber : 1,
                PageSize = request.PagedQuery != null ? request.PagedQuery.PageSize : listData.Count()
            }
        };
    }

    public IQueryable<Account> GetPagedQueryOperator(IQueryable<Account> query, GetPlayersQuery request, bool? downloadReport)
    {
        if (!downloadReport.HasValue)
        {
            if (request.PagedQuery.PageNumber > 0)
                query = query.Skip(request.PagedQuery.PageNumber * request.PagedQuery.PageSize);

            query = query.Take(request.PagedQuery.PageSize);
        }

        return query;
    }
    
    public IQueryable<Account> GetFilteredQueryOperator(IQueryable<Account> query, GetPlayersQuery request)
    {
        if (request.BranchId != null)
            query = query.Where(q => q.BranchId == request.BranchId);

        if (request.PagedQuery != null && !string.IsNullOrEmpty(request.PagedQuery.Search))
            query = query.Where(q => (q.FirstName + " " + q.LastName).ToLower().Contains(request.PagedQuery.Search.ToLower()) || q.MobileNumber.Contains(request.PagedQuery.Search.ToLower()));

        if (request.DateFrom.HasValue && request.DateTo.HasValue)
            query = query.Where(q => q.CreatedOn.Date >= request.DateFrom.Value.Date && q.CreatedOn.Date <= request.DateTo.Value.Date);

        return query;
    }
}
