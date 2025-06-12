using Innovision.Core.Infrastructure.Interfaces;
using MediatR;

namespace Innovision.Core.Application.Requests.CreditWallets.Queries.GetAccountTransactions;

public class GetAccountTransactionsQueryHandler(IPaymentServicesApi paymentServicesApi) : IRequestHandler<GetAccountTransactionsQuery, object>
{
    private readonly IPaymentServicesApi _paymentServicesApi = paymentServicesApi;

    public async Task<object> Handle(GetAccountTransactionsQuery request, CancellationToken cancellationToken)
        => await _paymentServicesApi.GetAccountTransactionRequest(request.Data, cancellationToken);
}