namespace Innovision.Core.Application.Requests.Deposits.Queries.GetDepositStatus;


public record DepositStatusVm(IEnumerable<DepositStatusDto> DepositStatus)
{
    public int Count
    {
        get
        {
            return DepositStatus.Count();
        }
    }
}