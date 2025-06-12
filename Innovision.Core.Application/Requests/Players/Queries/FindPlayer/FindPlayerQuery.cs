using MediatR;

namespace Innovision.Core.Application.Requests.Players.Queries.FindPlayer;

public class FindPlayerQuery : IRequest<PlayerDto>
{
    public Guid UserId { get; set; }
    public Guid CompanyId { get; set; }
}
