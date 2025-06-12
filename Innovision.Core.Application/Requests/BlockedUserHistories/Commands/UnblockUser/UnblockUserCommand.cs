using Innovision.Core.Application.Requests.BlockedUserHistories.Queries;
using MediatR;

namespace Innovision.Core.Application.Requests.BlockedUserHistories.Commands.UnblockUser;

public class UnblockUserCommand : IRequest<BlockUserDto>
{
  public long AccountInfoId { get; set; }
}
