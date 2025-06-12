using MediatR;

namespace Innovision.Core.Application.Requests.Administratives.Queries.GetAdminExclusionsExport;

public record GetAdminExclusionsExportQuery(int? Status = 1) : IRequest<AdminExclusionsFile>;
