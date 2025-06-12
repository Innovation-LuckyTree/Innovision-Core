using MediatR;

namespace Innovision.Core.Application.Requests.Users.Queries.GetFullyVerifiedUsersExport;

public record GetFullyVerifiedUsersExportQuery(int? CompanyId, DateTime? DateFrom, DateTime? DateTo) : IRequest<FullyVerifiedUsersFile>;
