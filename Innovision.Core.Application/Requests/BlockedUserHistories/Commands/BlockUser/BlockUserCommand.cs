using Innovision.Core.Application.Requests.BlockedUserHistories.Queries;
using MediatR;

namespace Innovision.Core.Application.Requests.BlockedUserHistories.Commands.BlockUser;

public class BlockUserCommand : IRequest<BlockUserDto>
{
  public long AccountInfoId { get; set; }
  public string? Remarks { get; set; }
}
