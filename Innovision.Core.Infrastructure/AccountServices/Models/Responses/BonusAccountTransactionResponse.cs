namespace Innovision.Core.Infrastructure.AccountServices.Models.Responses;

public class BonusAccountTransactionResponse
{
    public Guid AccountId { get; set; }
    public long PromotionId { get; set; }
    public  DateTimeOffset DateStart { get; set; }
    public  DateTimeOffset DateExpired { get; set; }
    public decimal ConsumedAmount { get; set; }
    public decimal Balance { get; set; }

    public IEnumerable<BonusAccountTransactionInfo> AccountTransactions { get; set; }
}
