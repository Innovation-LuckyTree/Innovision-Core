using MediatR;

namespace Innovision.Core.Application.Requests.Players.Queries.GetOfflinePlayersExport;

public record GetOfflinePlayersExportQuery(Guid CompanyObjId) : IRequest<OfflinePlayersFile>;
