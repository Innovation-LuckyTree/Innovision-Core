using MediatR;

namespace Innovision.Core.Application.Requests.ApplicationVersions.Queries.GetAdminExclusionById;

public record GetAdminExclusionByIdQuery(int AdministrativeExclusionId) : IRequest<AdministrativeExclusionDto>;
