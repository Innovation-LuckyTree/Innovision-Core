using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Users.AccountApproval.Queries.GetUsersForApprove;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.AgentAndPlayer.Queries.GetDownlinePlayers;

public class GetDownlinePlayersQuery() : IRequest<ApiResponse<DownlinePlayersVm>>
{
    public PagedQuery? PagedQuery { get; set; }
}
public class GetDownlinePlayersQueryHandler : IRequestHandler<GetDownlinePlayersQuery, ApiResponse<DownlinePlayersVm>>
{
    private readonly ICoreDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetDownlinePlayersQueryHandler(ICoreDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<ApiResponse<DownlinePlayersVm>> Handle(GetDownlinePlayersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var account = _dbContext.Accounts.Where(x => x.UserId == _currentUserService.UserObjId).FirstOrDefault();

            if (account == null)
                return new ApiResponse<DownlinePlayersVm>() { Success = false, ErrorMessage = "Account not found." };

            var query = _dbContext.Accounts.Where(m => m.RefferralCode == account.RefferralKey
                && m.UserTypeId == UserTypes.Player).AsQueryable();

            var totalCount = query.Count();

            if (request.PagedQuery != null)
                query = QueryFilter(query, request.PagedQuery);

            var queryResults = await query
                .ProjectTo<DownlinePlayersDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync(cancellationToken);

            return new ApiResponse<DownlinePlayersVm> { Data = new DownlinePlayersVm
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
            return new ApiResponse<DownlinePlayersVm> { Success = false, ErrorMessage = ex.Message };
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
