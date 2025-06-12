namespace Innovision.Core.Infrastructure.AccountServices.Models.Responses;

public class AccountBalanceResponse
{
    public Guid AccountId { get; set; }
    public string AccountType { get; set; }
    public decimal Balance { get; set; }
}