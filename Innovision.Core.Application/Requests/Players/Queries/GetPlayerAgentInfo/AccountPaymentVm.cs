namespace Innovision.Core.Application.Requests.Players.Queries.GetCurrentPlayerAgentInfo;

public class AccountPaymentVm
{
    public AccountPaymentDto Agent { get; set; }
    public AccountPaymentDto Player { get; set; }
    public string CompanyName { get; set; }
    public string BranchName { get; set; }
}