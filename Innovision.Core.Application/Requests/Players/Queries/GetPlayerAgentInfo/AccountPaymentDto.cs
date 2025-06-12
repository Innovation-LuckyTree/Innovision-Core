namespace Innovision.Core.Application.Requests.Players.Queries.GetCurrentPlayerAgentInfo;

public class AccountPaymentDto
{
    public long AccountId { get; set; }
    public Guid AccountObjId { get; set; }
    public string AccountName { get; set; }
    public string AccountType { get; set; }
    public string ReferralKey { get; set; }
}
