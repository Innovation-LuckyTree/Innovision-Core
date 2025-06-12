using MediatR;

namespace Innovision.Core.Application.Requests.ApplicationVersions.Commands.UpdateAdministrativeExclusion;

public record UpdateAdministrativeExclusionCommand(int AdministrativeExclusionId, int Status) : IRequest<AdministrativeExclusionDto>;
