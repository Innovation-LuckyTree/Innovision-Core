using MediatR;

namespace Innovision.Core.Application.Requests.PlayerActivities.Queries.GetClosedDrawInactivePlayers;

public class GetClosedDrawInactivePlayersQuery : IRequest<List<long>>
{
  public List<long> ActivePlayers { get; set; }
}
