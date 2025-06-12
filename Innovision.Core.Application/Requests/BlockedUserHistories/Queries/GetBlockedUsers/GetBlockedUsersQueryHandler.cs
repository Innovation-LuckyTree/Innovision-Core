using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.BlockedUserHistories.Queries.GetBlockedUsers;

public class GetBlockedUsersQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetBlockedUsersListQuery, BlockUsersVm>
{
  private readonly ICoreDbContext _coreDbContext = coreDbContext;
  private readonly IMapper _mapper = mapper;

  public async Task<BlockUsersVm> Handle(GetBlockedUsersListQuery request, CancellationToken cancellationToken)
  {

    var query = _coreDbContext.BlockedUserHistories
        .Include(x => x.Account)
        .Where(x => x.IsActive == 1)
        .OrderByDescending(x => x.BlockedDate)
        .AsQueryable();

    if (!string.IsNullOrEmpty(request.PagedQuery?.Search))
    {
      query = query.Where(o => o.Account.FirstName.Contains(request.PagedQuery.Search)
          || o.Account.LastName.Contains(request.PagedQuery.Search));
    }

    var totalCount = await query.CountAsync(cancellationToken);

    if (request.PagedQuery == null)
    {
      // return all data if PagedQuery is null
      var allBlockedUsers = await query
          .ProjectTo<BlockUserDto>(_mapper.ConfigurationProvider)
          .ToListAsync(cancellationToken);

      return new BlockUsersVm(allBlockedUsers)
      {
        Offset = 0,
        TotalCount = totalCount,
        PageSize = totalCount
      };
    }

    var pageSize = request.PagedQuery.PageSize;
    var pageNumber = request.PagedQuery.PageNumber;
    var start = pageNumber * pageSize;

    var blockedUsers = await query
        .Skip(start)
        .Take(pageSize)
        .ProjectTo<BlockUserDto>(_mapper.ConfigurationProvider)
        .ToListAsync(cancellationToken);

    return new BlockUsersVm(blockedUsers)
    {
      Offset = start,
      TotalCount = totalCount,
      PageSize = pageSize
    };
  }
}
