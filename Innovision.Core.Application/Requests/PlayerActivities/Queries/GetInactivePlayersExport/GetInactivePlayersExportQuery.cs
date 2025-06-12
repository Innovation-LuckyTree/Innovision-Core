using MediatR;

namespace Innovision.Core.Application.Requests.PlayerActivities.Queries.GetInactivePlayersExport;

public record GetInactivePlayersExportQuery() : IRequest<InactivePlayersFile>;
