using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.PlayerActivities.Queries.GetClosedDrawInactivePlayers;

public class GetClosedDrawInactivePlayersQueryHandler : IRequestHandler<GetClosedDrawInactivePlayersQuery, List<long>>
{
  private readonly ICoreDbContext _coreDbContext;

  public GetClosedDrawInactivePlayersQueryHandler(ICoreDbContext coreDbContext)
  {
    _coreDbContext = coreDbContext;
  }

  public async Task<List<long>> Handle(GetClosedDrawInactivePlayersQuery request, CancellationToken cancellationToken)
  {
    var query = await _coreDbContext.Accounts
        .Where(m => m.UserTypeId == UserTypes.Player && !request.ActivePlayers.Contains(m.AccountInfoId))
        .Select(m => m.AccountInfoId)
        .ToListAsync(cancellationToken);

    return query;
  }
}
