using MediatR;

namespace Innovision.Core.Application.Requests.Players.Queries.GetLockedUsersExport;

public record GetLockedUsersExportQuery(Guid CompanyObjectId) : IRequest<LockedUsersFile>;