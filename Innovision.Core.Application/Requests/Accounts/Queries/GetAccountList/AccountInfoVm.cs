using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetAccountList;

public record AccountInfoVm(IEnumerable<AccountInfoDto> Accounts);
