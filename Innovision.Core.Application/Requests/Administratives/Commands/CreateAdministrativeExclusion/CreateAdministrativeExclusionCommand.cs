using MediatR;

namespace Innovision.Core.Application.Requests.ApplicationVersions.Commands.CreateAdministrativeExclusion;

public record CreateAdministrativeExclusionCommand(long AccountId, int DayDuration, TimeSpan TimeDuration) : IRequest<AdministrativeExclusionDto>;
