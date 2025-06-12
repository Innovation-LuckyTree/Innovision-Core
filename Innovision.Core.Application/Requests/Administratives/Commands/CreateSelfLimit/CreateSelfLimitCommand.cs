using MediatR;

namespace Innovision.Core.Application.Requests.ApplicationVersions.Commands.CreateSelfLimit;

public record CreateSelfLimitCommand(long AccountId, decimal AmountLimit) : IRequest<SelfLimitDto>;
