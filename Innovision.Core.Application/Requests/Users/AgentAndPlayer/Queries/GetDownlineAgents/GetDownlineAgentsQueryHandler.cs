using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.AgentAndPlayer.Queries.GetDownlineAgents;

public class GetDownlineAgentsQuery() : IRequest<ApiResponse<DownlineAgentVm>>
{
    public PagedQuery? PagedQuery { get; set; }
}
public class GetDownlineAgentsQueryHandler : IRequestHandler<GetDownlineAgentsQuery, ApiResponse<DownlineAgentVm>>
{
    private readonly ICoreDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMediator _mediator;

    public GetDownlineAgentsQueryHandler(ICoreDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService, IMediator mediator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _mediator = mediator;
    }

    public async Task<ApiResponse<DownlineAgentVm>> Handle(GetDownlineAgentsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var account = _dbContext.Accounts.Where(x => x.UserId == _currentUserService.UserObjId).FirstOrDefault();

            if (account == null)
                return new ApiResponse<DownlineAgentVm>() { Success = false, ErrorMessage = "Account not found." };

            var query = _dbContext.Accounts.Where(m => m.RefferralCode == account.RefferralKey
                && m.UserTypeId == UserTypes.Agent).AsQueryable();

            var totalCount = query.Count();

            if (request.PagedQuery != null)
                query = QueryFilter(query, request.PagedQuery);

            var queryResults = await query
                .ProjectTo<DownlineAgentsDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync(cancellationToken);

            // Get all players
            var players = await _dbContext.Accounts.Where(m => queryResults.Select(m => m.RefferalKey).Contains(m.RefferralCode)
                && m.UserTypeId == Domain.Enums.UserTypes.Player)
                .Select(m => m.RefferralCode).ToListAsync();

            foreach (var item in queryResults)
            {
                //var agentsCount = agents.Where(m => m == item.RefferalKey).Count();
                var playersCount = players.Where(m => m == item.RefferalKey).Count();

                //item.AgentsCount = agentsCount;
                item.PlayersCount = playersCount;
            }

            return new ApiResponse<DownlineAgentVm>() { Data = new DownlineAgentVm
                {
                    Results = queryResults,
                    Total = totalCount,
                    PageNumber = (request.PagedQuery != null) ? request.PagedQuery.PageNumber : 1,
                    PageSize = (request.PagedQuery != null) ? request.PagedQuery.PageSize : queryResults.Count()
                }
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<DownlineAgentVm>() { Success = false, ErrorMessage = ex.Message };
        }
    }

    public IQueryable<Account> QueryFilter(IQueryable<Account> query, PagedQuery pagedQuery)
    {
        if (!string.IsNullOrEmpty(pagedQuery.Search))
            query = query.Where(q => (q.FirstName.ToLower() + " " + q.LastName.ToLower()).Contains(pagedQuery.Search.ToLower()));

        if (pagedQuery.PageNumber > 0)
            query = query.Skip((pagedQuery.PageNumber) * pagedQuery.PageSize);

        query = query.Take(pagedQuery.PageSize);

        return query;
    }
}
