namespace Innovision.Core.Application.Requests.Accounts.Users.Operator;

public class OperatorDto
{
    public int OperatorId { get; set; }
    public long AccountInfoId { get; set; }
    public bool IsMain { get; set; }
}