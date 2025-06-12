using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.BlockedUserHistories.Queries.GetBlockedUsers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.BlockedUserHistories.Queries.GetUsersBlockHistories;

public class GetUsersBlockHistoriesQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetUsersBlockHistoriesQuery, BlockUsersVm>
{
  private readonly ICoreDbContext _coreDbContext = coreDbContext;
  private readonly IMapper _mapper = mapper;

  public async Task<BlockUsersVm> Handle(GetUsersBlockHistoriesQuery request, CancellationToken cancellationToken)
  {
    var query = _coreDbContext.BlockedUserHistories
            .Include(x => x.Account)
            .Where(x => x.AccountInfoId == request.AccountInfoId)
            .OrderByDescending(x => x.BlockedDate)
            .AsQueryable();

    var totalCount = await query.CountAsync(cancellationToken);

    // no pagination, return all data
    var allBlockedUsers = await query
        .ProjectTo<BlockUserDto>(_mapper.ConfigurationProvider)
        .ToListAsync(cancellationToken);

    return new BlockUsersVm(allBlockedUsers)
    {
      Offset = allBlockedUsers.Count,
      TotalCount = totalCount,
      PageSize = allBlockedUsers.Count
    };
  }
}