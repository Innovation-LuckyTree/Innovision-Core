using MediatR;

namespace Innovision.Core.Application.Requests.Deposits.Queries.LookupReference;

public record LookupReferenceQuery(string TransactionNo) : IRequest<DepositDto>;

