using MediatR;

namespace Innovision.Core.Application.Requests.Players.Queries.GetOnlinePlayersExport;

public record GetOnlinePlayersExportQuery(Guid CompanyObjId) : IRequest<OnlinePlayersFile>;
