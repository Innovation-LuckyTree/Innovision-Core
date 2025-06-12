using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.Queries.GetPlayerByObjectId;

public record GetPlayerNumberByObjectIdQuery(Guid AccountObjctId) : IRequest<ApiResponse<string>>;
