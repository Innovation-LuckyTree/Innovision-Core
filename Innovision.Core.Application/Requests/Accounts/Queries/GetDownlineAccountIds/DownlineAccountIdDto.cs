using System.Text.Json.Serialization;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetDownlineAccountIds;

public record DownlineAccountIdDto([property: JsonIgnore] IEnumerable<DownlineAccountInfo> Accounts)
{
    public IEnumerable<long> AccountIds
    {
        get
        {
            if ((Accounts?.Count() ?? 0) == 0)
                return [];

            return Accounts.Select(o => o.AccountInfoId);
        }
    }
    public int Count { get => AccountIds?.Count() ?? 0; }
}
