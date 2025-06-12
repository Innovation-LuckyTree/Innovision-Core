using MediatR;

namespace Innovision.Core.Application.Requests.Administratives.Queries.GetSelfLimitsExport;

public record GetSelfLimitsExportQuery(int? Status = 1) : IRequest<SelfLimitsFile>;
