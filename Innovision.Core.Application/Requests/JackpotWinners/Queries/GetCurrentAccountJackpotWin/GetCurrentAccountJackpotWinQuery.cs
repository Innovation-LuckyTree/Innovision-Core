using Innovision.Core.Application.Common;
using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.JackpotWinners.Queries.GetCurrentAccountJackpotWin;

public record GetCurrentAccountJackpotWinQuery(int? Status, PagedQuery PagedQuery) : IRequest<ApiResponse<AccountJackpotWinVm>>;

