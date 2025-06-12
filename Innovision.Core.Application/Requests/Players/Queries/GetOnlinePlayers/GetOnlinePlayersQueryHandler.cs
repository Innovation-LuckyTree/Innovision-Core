using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Users.Queries;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Players.Queries.GetOnlinePlayers;

public class GetOnlinePlayersQueryHandler(ICoreDbContext dbContext, IMapper mapper, IUserStatusServices userStatusServices) : IRequestHandler<GetOnlinePlayersQuery, ApiResponse<UserStatusVm>>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly IUserStatusServices _userStatusServices = userStatusServices;

    public async Task<ApiResponse<UserStatusVm>> Handle(GetOnlinePlayersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var onlineIds = await _userStatusServices.GetOnlineIds(cancellationToken);
            var playingIds = await _userStatusServices.GetPlayingIds(request.CompanyObjId, cancellationToken);

            var online = onlineIds.Where(m => !playingIds.Contains(m)).ToList();

            var query = _dbContext.Accounts
                .Include(m => m.UserType)
                .Include(m => m.Branch)
                .Where(m => online.Contains(m.AccountInfoId))
                .OrderBy(x => x.AccountInfoId)
                .AsQueryable();

            if (request.PagedQuery != null)
                query = FilterQuery(query, request.PagedQuery);

            var userslist = await query
                .ProjectTo<UserStatusDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ApiResponse<UserStatusVm>()
            {
                Data = new UserStatusVm
                {
                    Results = userslist,
                    Total = online?.Count() ?? 0,
                    PageNumber = request.PagedQuery != null ? request.PagedQuery.PageNumber : 1,
                    PageSize = request.PagedQuery != null ? request.PagedQuery.PageSize : userslist.Count()
                }
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<UserStatusVm>() { Success = false, ErrorMessage = ex.Message };
        }
    }

    public IQueryable<Account> FilterQuery(IQueryable<Account> query, PagedQuery pagedQuery)
    {
        if (!string.IsNullOrEmpty(pagedQuery.Search))
        {
            query = query.Where(q => q.FirstName.ToLower().Contains(pagedQuery.Search.ToLower())
                || q.LastName.ToLower().Contains(pagedQuery.Search.ToLower())
                || q.MobileNumber.Contains(pagedQuery.Search));
        }

        if (pagedQuery.PageNumber > 0)
            query = query.Skip(pagedQuery.PageNumber * pagedQuery.PageSize);

        query = query.Take(pagedQuery.PageSize);

        return query;
    }
}
