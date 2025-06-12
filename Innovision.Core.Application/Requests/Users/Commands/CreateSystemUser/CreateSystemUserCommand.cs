using Innovision.Core.Application.Common;
using Innovision.Core.Application.Requests.Accounts.Users.AgentAndPlayer.Commands.UserRegistration;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.Commands.CreateSystemUser;
public class CreateSystemUserCommand : IRequest<ApiResponse<Guid>>
{
    public int RoleId { get; set; }
    public UserRegistrationCommand UserModel { get; set; }
}
