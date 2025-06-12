using MediatR;

namespace Innovision.Core.Application.Requests.ApplicationVersions.Queries.GetSelfLimitById;

public record GetSelfLimitByIdQuery(int SelfLimitId) : IRequest<SelfLimitDto>;
