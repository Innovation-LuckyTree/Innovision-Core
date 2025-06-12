namespace Innovision.Core.Infrastructure.AccountServices.Models.Requests;

public record ProcessReturnBonusRequest(Guid AccountId, long PromotionId, string DateStart, string DateExpired, bool IsExpire, string TransactionNo, decimal? Amount);
