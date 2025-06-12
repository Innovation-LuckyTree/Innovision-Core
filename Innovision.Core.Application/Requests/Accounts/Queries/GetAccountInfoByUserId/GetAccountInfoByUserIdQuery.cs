using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;
using MediatR;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetAccountInfoByUserId;

public record GetAccountInfoByUserIdQuery(Guid UserId) : IRequest<AccountInfoDto>
{
}
