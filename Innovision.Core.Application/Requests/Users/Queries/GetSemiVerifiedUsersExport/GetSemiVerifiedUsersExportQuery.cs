using MediatR;

namespace Innovision.Core.Application.Requests.Users.Queries.GetSemiVerifiedUsersExport;

public record GetSemiVerifiedUsersExportQuery(int? CompanyId, DateTime? DateFrom, DateTime? DateTo) : IRequest<SemiVerifiedUsersFile>;
