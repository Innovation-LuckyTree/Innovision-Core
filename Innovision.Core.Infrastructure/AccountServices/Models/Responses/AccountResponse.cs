namespace Innovision.Core.Infrastructure.AccountServices.Models.Responses;


public class AccountResponse : WalletBaseResponse<AccountDto>
{
}

public class AccountDto
{
    public string Id { get; set; }
    public string Name { get; set; }

    public string Email { get; set; }
    public string MobileNumber { get; set; }

    public long TransactionCount { get; set; }
    public  DateTimeOffset CreatedAt { get; set; }

    public  DateTimeOffset UpdateAt { get; set; }
}
