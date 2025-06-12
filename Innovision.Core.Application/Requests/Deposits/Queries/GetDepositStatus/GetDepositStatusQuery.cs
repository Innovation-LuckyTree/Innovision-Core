using MediatR;

namespace Innovision.Core.Application.Requests.Deposits.Queries.GetDepositStatus;

public class GetDepositStatusQuery : IRequest<DepositStatusVm>;
