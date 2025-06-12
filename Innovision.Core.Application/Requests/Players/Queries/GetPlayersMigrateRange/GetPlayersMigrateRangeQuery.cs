using Innovision.Core.Application.Common;
using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.Players.Queries.GetPlayersMigrateRange;

public record GetPlayersMigrateRangeQuery(DateTime CreatedDateFrom, DateTime CreatedDateTo, DateTime? ModifiedDateFrom, DateTime? ModifiedDateTo, PagedQuery? PagedQuery) : IRequest<ApiResponse<GetPlayerMigrateRangeVM>>;
