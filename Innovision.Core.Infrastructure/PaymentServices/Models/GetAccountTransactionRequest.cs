namespace Innovision.Core.Infrastructure.PaymentServices.Models.Requests;

public class GetAccountTransactionRequest
{
    public Guid AccountId { get; set; }
    public string? SearchKey { get; set; }
    public int? TransactionType { get; set; }
    public int? Start { get; set; } = 0;
    public int? PageSize { get; set; } = 10;
    public  DateTimeOffset? StartDate { get; set; }
    public  DateTimeOffset? EndDate { get; set; }
}
