namespace Innovision.Core.Infrastructure.AccountServices.Models.Requests;

public record AddCreditTransactionRequest(string TransactionNo, decimal Amount, string? Notes);
