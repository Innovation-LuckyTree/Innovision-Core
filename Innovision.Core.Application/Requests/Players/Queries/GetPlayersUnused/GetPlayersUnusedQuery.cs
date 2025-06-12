using Innovision.Core.Application.Common;
using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.Players.Queries.GetPlayersUnusedQuery;

public record GetPlayersUnusedQuery(DateTime CreatedDateFrom, DateTime CreatedDateTo, PagedQuery? PagedQuery) : IRequest<ApiResponse<GetPlayersUnusedVM>>;
