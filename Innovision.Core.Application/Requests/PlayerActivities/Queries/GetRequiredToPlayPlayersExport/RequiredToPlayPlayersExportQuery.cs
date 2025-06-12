using MediatR;

namespace Innovision.Core.Application.Requests.PlayerActivities.Queries.GetRequiredToPlayPlayersExport;

public record GetRequiredToPlayPlayersExportQuery() : IRequest<RequiredToPlayPlayersFile>;