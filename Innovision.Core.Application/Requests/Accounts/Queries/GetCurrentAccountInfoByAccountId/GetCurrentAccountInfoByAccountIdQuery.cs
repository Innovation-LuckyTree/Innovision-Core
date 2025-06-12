using MediatR;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;

public record GetCurrentAccountInfoByAccountIdQuery(Guid AccountObjectId) : IRequest<AccountInfoDto>;
