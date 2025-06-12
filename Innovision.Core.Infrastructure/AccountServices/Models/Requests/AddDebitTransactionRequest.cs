namespace Innovision.Core.Infrastructure.AccountServices.Models.Requests;

public record AddDebitTransactionRequest(string TransactionNo, decimal Amount, string Notes);