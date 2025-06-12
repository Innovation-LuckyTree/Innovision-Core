using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccount;

namespace Innovision.Core.Application.Common.Models.Responses;

public class CurrentAccountResponse
{
    public List<MenuDetailsDto> AcocuntMenus { get; set; }
    public CurrentAccountDto Account { get; set; }
}
