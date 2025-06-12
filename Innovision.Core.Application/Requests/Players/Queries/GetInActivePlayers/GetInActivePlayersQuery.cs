using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Domain.Enums;
using Innovision.Core.Infrastructure.Games.Models.Requests;
using Innovision.Core.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Players.Queries.GetInActivePlayers
{
    public record GetInActivePlayersQuery(Guid CompanyObjectId, PagedQuery? PagedQuery) : IRequest<ApiResponse<InActivityVm>>;
    public class GetInActivePlayersQueryHandler(ICoreDbContext dbContext, IMapper mapper, IGamesApi gamesApi) : IRequestHandler<GetInActivePlayersQuery, ApiResponse<InActivityVm>>
    {
        private readonly ICoreDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;
        private readonly IGamesApi _gamesApi = gamesApi;

        public async Task<ApiResponse<InActivityVm>> Handle(GetInActivePlayersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<long> guids = new List<long>();
                var curBetUsers = await _gamesApi.GetCurrentBetUsers(new PlayingNowRequest { CompanyId = request.CompanyObjectId, Start = 0, Size = 1000 }, cancellationToken);

                if (curBetUsers == null)
                    return new ApiResponse<InActivityVm>() { Data = new InActivityVm() };

                if (curBetUsers.Data != null)
                    guids.AddRange(curBetUsers.Data.Accounts.Select(m => m.AccountId).ToList());

                // query next page by 1000
                if (curBetUsers.Total > 1000)
                {
                    // round if any decimal point
                    var lprange = Math.Round(Convert.ToDecimal(curBetUsers.Total / 1000), MidpointRounding.AwayFromZero) - 1;
                    for (int i = 1; i < lprange; i++)
                    {
                        var curBetUsers1 = await _gamesApi.GetCurrentBetUsers(new PlayingNowRequest { CompanyId = request.CompanyObjectId, Start = i, Size = 1000 }, cancellationToken);
                        if (curBetUsers1 == null)
                            continue;
                        if (curBetUsers1.Data.Accounts.Count == 0)
                            continue;

                        guids.AddRange(curBetUsers1.Data.Accounts.Select(m => m.AccountId).ToList());
                    }
                }

                var query = _dbContext.Accounts
                    .Include(m => m.UserType)
                    .Include(m => m.Branch)
                    .Include(m => m.Orders)
                    .Where(m => !guids.Contains(m.AccountInfoId) && m.UserTypeId == UserTypes.Player)
                    .OrderBy(x => x.AccountInfoId)
                    .AsQueryable();

                var total = await query.CountAsync();

                if (request.PagedQuery != null)
                    query = FilterQuery(query, request.PagedQuery);

                var userslist = await query
                    .ProjectTo<InActivityDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return new ApiResponse<InActivityVm>()
                {
                    Data = new InActivityVm
                    {
                        CurrentDrawTime = (curBetUsers.Data != null) ? curBetUsers.Data.DrawTime : "N/A",
                        Results = userslist,
                        Total = total,
                        PageNumber = request.PagedQuery != null ? request.PagedQuery.PageNumber : 1,
                        PageSize = request.PagedQuery != null ? request.PagedQuery.PageSize : userslist.Count()
                    }
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<InActivityVm>() { Success = false, ErrorMessage = ex.Message };
            }
        }

        public IQueryable<Account> FilterQuery(IQueryable<Account> query, PagedQuery pagedQuery)
        {
            if (!string.IsNullOrEmpty(pagedQuery.Search))
                query = query.Where(q => (q.FirstName + " " + q.LastName).ToLower().Contains(pagedQuery.Search.ToLower()));

            if (pagedQuery.PageNumber > 0)
                query = query.Skip((pagedQuery.PageNumber) * pagedQuery.PageSize);

            query = query.Take(pagedQuery.PageSize);

            return query;
        }
    }
}
