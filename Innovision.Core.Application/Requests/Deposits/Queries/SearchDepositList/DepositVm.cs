namespace Innovision.Core.Application.Requests.Deposits.Queries.SearchDepositList;

public record DepositVm(IEnumerable<DepositDto> Deposits)
{
    public int Offset { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    public int PageNumber { get; set; }
    public int Count
    {
        get
        {
            return Deposits.Count();
        }
    }
}