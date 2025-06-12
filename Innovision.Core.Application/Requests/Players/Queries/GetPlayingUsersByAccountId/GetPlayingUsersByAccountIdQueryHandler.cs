using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Users.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Players.Queries.GetPlayingUsersByAccountId;

public class GetPlayingUsersByAccountIdQueryHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<GetPlayingUsersByAccountIdQuery, ApiResponse<IEnumerable<UserStatusDto>>>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiResponse<IEnumerable<UserStatusDto>>> Handle(GetPlayingUsersByAccountIdQuery request, CancellationToken cancellationToken)
    {
        if ((request?.AccountIds?.Count() ?? 0) == 0)
            return new ApiResponse<IEnumerable<UserStatusDto>>();

        var query = _dbContext.Accounts
            .Include(m => m.UserType)
            .Include(m => m.Branch)
            .Where(m => request.AccountIds.Contains(m.AccountInfoId))
            .OrderBy(x => x.AccountInfoId)
            .AsQueryable();

        var userslist = await query
            .ProjectTo<UserStatusDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new ApiResponse<IEnumerable<UserStatusDto>>()
        {
            Data = userslist
        };
    }
}
