using MediatR;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetAccountList;

public record GetAccountListQuery(IEnumerable<long> AccountIds) : IRequest<AccountInfoVm>
{
}
