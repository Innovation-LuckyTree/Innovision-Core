namespace Innovision.Core.Infrastructure.AccountServices.Models.Requests;

public record BonusAccountTransactionRequest(Guid AccountId, long PromotionId, string DateStart, string DateExpired);
