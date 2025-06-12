using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Users.AgentAndPlayer.Queries.GetDownlineAgents;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Accounts.Users.AgentAndPlayer.Queries.GetAgents;

public class GetAgentsQueryHandler : IRequestHandler<GetAgentsQuery, ApiResponse<DownlineAgentVm>>
{
    private readonly IMapper _mapper;
    private readonly ICoreDbContext _dbContext;
    private readonly IMediator _mediator;

    public GetAgentsQueryHandler(ICoreDbContext dbContext, IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _mediator = mediator;
    }

    public async Task<ApiResponse<DownlineAgentVm>> Handle(GetAgentsQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Accounts
            .Include(m => m.Branch)
            .Where(x => x.UserTypeId == Domain.Enums.UserTypes.Agent)
            .OrderByDescending(m => m.CreatedOn).AsQueryable();

        if (request.BranchId != null)
            query = query.Where(q => q.BranchId == request.BranchId);

        var total = await query.CountAsync();

        query = GetPagedQueryOperator(query, request);

        var listData = await query
            .ProjectTo<DownlineAgentsDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        // Get all agents
        var agents = await _dbContext.Accounts.Where(m => listData.Select(m => m.RefferalKey).Contains(m.RefferralCode)
            && m.UserTypeId == Domain.Enums.UserTypes.Agent)
            .Select(m => m.RefferralCode).ToListAsync();

        // Get all players
        var players = await _dbContext.Accounts.Where(m => listData.Select(m => m.RefferalKey).Contains(m.RefferralCode)
            && m.UserTypeId == Domain.Enums.UserTypes.Player)
            .Select(m => m.RefferralCode).ToListAsync();

        // Get all players
        var uplines = await _dbContext.Accounts.Where(m => listData.Select(m => m.RefferalCode).Contains(m.RefferralKey))
            .ToListAsync();

        foreach (var item in listData)
        {
            var upline = uplines.Where(m => m.RefferralKey == item.RefferalCode).FirstOrDefault();
            var agentsCount = agents.Where(m => m == item.RefferalKey).Count();
            var playersCount = players.Where(m => m == item.RefferalKey).Count();

            item.AgentsCount = agentsCount;
            item.PlayersCount = playersCount;
            item.RecruiterName = (upline != null) ? $"{upline.FirstName} {upline.LastName}" : "";
        }

        return new ApiResponse<DownlineAgentVm>
        {
            Data = new DownlineAgentVm
            {
                Results = listData,
                Total = total,
                PageNumber = request.PagedQuery != null ? request.PagedQuery.PageNumber : 1,
                PageSize = request.PagedQuery != null ? request.PagedQuery.PageSize : listData.Count()
            }
        };
    }

    public IQueryable<Account> GetPagedQueryOperator(IQueryable<Account> query, GetAgentsQuery request)
    {
        if (!string.IsNullOrEmpty(request.PagedQuery.Search))
            query = query.Where(q => (q.FirstName + " " + q.LastName).ToLower().Contains(request.PagedQuery.Search.ToLower()) || q.MobileNumber.Contains(request.PagedQuery.Search.ToLower()));

        if (request.PagedQuery.PageNumber > 0)
            query = query.Skip(request.PagedQuery.PageNumber * request.PagedQuery.PageSize);

        if (request.DateFrom.HasValue && request.DateTo.HasValue)
            query = query.Where(q => q.CreatedOn >= request.DateFrom && q.CreatedOn <= request.DateTo);

        if (request.DateFrom.HasValue)
            query = query.Where(q => q.CreatedOn == request.DateFrom);

        query = query.Take(request.PagedQuery.PageSize);

        return query;
    }
}
