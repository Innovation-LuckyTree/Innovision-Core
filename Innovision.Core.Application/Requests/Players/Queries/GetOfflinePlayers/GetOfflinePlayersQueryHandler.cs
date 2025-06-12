using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Users.Queries;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Players.Queries.GetOfflinePlayers;

public class GetOfflinePlayersQueryHandler(ICoreDbContext dbContext, IMapper mapper, IUserStatusServices userStatusServices) : IRequestHandler<GetOfflinePlayersQuery, ApiResponse<UserStatusVm>>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly IUserStatusServices _userStatusServices = userStatusServices;

    public async Task<ApiResponse<UserStatusVm>> Handle(GetOfflinePlayersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var onlineIds = await _userStatusServices.GetOnlineIds(cancellationToken);
            var playingIds = await _userStatusServices.GetPlayingIds(request.CompanyObjId, cancellationToken);

            var online = onlineIds.Union(playingIds);

            var query = _dbContext.Accounts
                .Include(m => m.UserType)
                .Include(m => m.Branch)
                .Where(m => !online.Contains(m.AccountInfoId) && m.UserTypeId == UserTypes.Player)
                .OrderBy(x => x.AccountInfoId)
                .AsQueryable();

            //if (request.CompanyId != null)
            //    query = query.Where(m => m.Branch.CompanyId == request.CompanyId);

            var total = await query.CountAsync();

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
                    Total = total,
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
            query = query.Skip((pagedQuery.PageNumber) * pagedQuery.PageSize);

        query = query.Take(pagedQuery.PageSize);

        return query;
    }
}
