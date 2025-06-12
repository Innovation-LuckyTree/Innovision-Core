using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.JackpotWinners.Queries.GetJackpotWinnerList;

public record GetJackpotWinnerListQuery(GetJackpotWinnerListRequest Request, long CompanyGameId) : IRequest<ApiResponse<PaginateResult<JackpotWinnerInfo>>>;
