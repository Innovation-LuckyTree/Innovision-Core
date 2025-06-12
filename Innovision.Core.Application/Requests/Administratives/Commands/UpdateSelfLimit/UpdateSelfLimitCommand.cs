using MediatR;

namespace Innovision.Core.Application.Requests.ApplicationVersions.Commands.UpdateSelfLimit;

public record UpdateSelfLimitCommand(int SelfLimitId, decimal AmountLimit, int Status = 1) : IRequest<SelfLimitDto>;
