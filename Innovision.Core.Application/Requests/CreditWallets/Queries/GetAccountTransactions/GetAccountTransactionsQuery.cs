using Innovision.Core.Infrastructure.PaymentServices.Models.Requests;
using MediatR;

namespace Innovision.Core.Application.Requests.CreditWallets.Queries.GetAccountTransactions;

public record GetAccountTransactionsQuery(GetAccountTransactionRequest Data) : IRequest<object>;
