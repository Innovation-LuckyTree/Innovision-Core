using MediatR;

namespace Innovision.Core.Application.Requests.Deposits.Queries.GetDepositById;

public record GetDepositByIdQuery(long DepositId) : IRequest<DepositDto>;
