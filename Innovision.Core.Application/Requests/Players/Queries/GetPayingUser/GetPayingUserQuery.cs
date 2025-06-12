using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Users.Queries;
using Innovision.Core.Common.Models;
using Innovision.Core.Infrastructure.Games.Models.Requests;
using Innovision.Core.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Players.Queries.GetPayingUser
{
    public record GetPayingUserQuery(Guid CompanyObjectId, PagedQuery? PagedQuery) : IRequest<ApiResponse<UserStatusVm>>;
    public class GetPayingUserQueryHandler(ICoreDbContext dbContext, IMapper mapper, IGamesApi gamesApi) : IRequestHandler<GetPayingUserQuery, ApiResponse<UserStatusVm>>
    {
        private readonly ICoreDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;
        private readonly IGamesApi _gamesApi = gamesApi;

        public async Task<ApiResponse<UserStatusVm>> Handle(GetPayingUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var curBetUsers = await _gamesApi.GetCurrentBetUsers(new PlayingNowRequest { 
                    CompanyId = request.CompanyObjectId,
                    Start = request.PagedQuery?.SkipCount ?? 0,
                    Size = request.PagedQuery?.PageSize ?? 10
                }, cancellationToken);

                if ((curBetUsers?.Total ?? 0) == 0)
                    return new ApiResponse<UserStatusVm>() { Data =  new UserStatusVm() };

                var query = _dbContext.Accounts
                    .Include(m => m.UserType)
                    .Include(m => m.Branch)
                    .Where(m => curBetUsers.Data.Accounts.Select(o => o.AccountId).Contains(m.AccountInfoId))
                    .OrderBy(x => x.AccountInfoId)
                    .AsQueryable();

                var userslist = await query
                    .ProjectTo<UserStatusDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                foreach (var item in userslist)
                {
                    var curBetUser = curBetUsers.Data.Accounts.Where(m => m.AccountId == item.AccountInfoId).FirstOrDefault();
                    item.BetCount = (curBetUser != null) ? curBetUser.BetCount : 0;
                }

                if (!string.IsNullOrEmpty(request.PagedQuery.Search))
                    userslist = userslist.Where(m => m.Fullname.ToLower().Contains(request.PagedQuery.Search.ToLower())).ToList();

                return new ApiResponse<UserStatusVm>()
                {
                    Data = new UserStatusVm
                    {
                        CurrentDrawTime = curBetUsers.Data.DrawTime,
                        Results = userslist,
                        Total = curBetUsers.Total,
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
    }
}
