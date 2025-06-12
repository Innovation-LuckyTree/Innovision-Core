using MediatR;

namespace Innovision.Core.Application.Requests.ApplicationVersions.Queries.GetAccountAdminExclusion;

public record GetAccountAdminExclusionQuery(long AccountId) : IRequest<AdministrativeExclusionDto>;
