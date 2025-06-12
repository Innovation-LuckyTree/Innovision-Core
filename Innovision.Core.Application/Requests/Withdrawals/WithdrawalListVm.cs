namespace Innovision.Core.Application.Requests.Withdrawals;

public class WithdrawalListVm
{
    public int Total { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public List<WithdrawalDto> WithdrawalList { get; set; }
}