using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Players.Queries.GetPlayerByCompanyId;

public record GetPlayerByCompanyIdQuery() : IRequest<ApiResponse<List<CompanyPlayerDto>>>;
