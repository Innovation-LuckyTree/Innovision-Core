using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.Operator.Queries.GetMainOperator;

public record GetMainOperatorQuery() : IRequest<ApiResponse<MainOperatorDto>>;
