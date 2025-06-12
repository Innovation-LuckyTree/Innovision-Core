namespace Innovision.Core.Infrastructure.AccountServices.Models.Responses;

public class BonusAccountByPromotionResponse : WalletBaseResponse<BonusAccountByPromotionVm>
{
}

public class BonusAccountByPromotionVm
{
    public Guid AccountId { get; set; }
    public string AccountType { get; set; }
    public decimal Balance { get; set; }
    public IEnumerable<PromotionDetailInfo> PromotionDetails { get; set; }
}

public class PromotionDetailInfo
{
    public long PromotionId { get; set; }
    public  DateTimeOffset DateStarted { get; set; }
    public  DateTimeOffset ExpirationDate { get; set; }
    public decimal RemainingAmount { get; set; }
    public decimal ConsumedAmount { get; set; }
}