namespace Innovision.Core.Infrastructure.PaymentServices.Models.Requests;

public record GetAccountBalanceRequest(IEnumerable<Guid> AccountGuiIds);
