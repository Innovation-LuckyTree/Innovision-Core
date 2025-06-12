using MediatR;

namespace Innovision.Core.Application.Requests.PlayerActivities.Queries.GetExtendedPlayersExport;

public record GetExtendedPlayersExportQuery() : IRequest<ExtendedPlayersFile>;