namespace Innovision.Core.Application.Requests.Accounts.Queries.GetApprovedAccounts;

public record AccountVm(IEnumerable<AccountDto> Accounts)
{
    public int Count
    {
        get
        {
            return Accounts.Count();
        }
    }
}