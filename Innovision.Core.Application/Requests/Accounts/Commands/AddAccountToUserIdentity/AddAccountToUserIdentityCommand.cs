using Innovision.Core.Infrastructure.CoreIdentity.Models.Responses;
using MediatR;

namespace Innovision.Core.Application.Requests.Accounts.Commands.AddAccountToUserIdentity;

public class AddAccountToUserIdentityCommand : IRequest<CreateUserResponse>
{
    public Guid AccountInfoId { get; set; }
    public string Password { get; set; }
}
