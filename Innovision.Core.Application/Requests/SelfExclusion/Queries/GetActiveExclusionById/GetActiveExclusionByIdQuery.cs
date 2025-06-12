using MediatR;

namespace Innovision.Core.Application.Requests.SelfExclusion.Queries.GetActiveExclusionById;

public record GetActiveExclusionByIdQuery(long AccountId) : IRequest<SelfExclusionDto> { }
