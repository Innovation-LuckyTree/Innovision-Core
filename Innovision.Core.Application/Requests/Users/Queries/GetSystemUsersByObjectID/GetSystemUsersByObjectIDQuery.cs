using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.Queries.GetSystemUsersByObjectID;

public record GetSystemUsersByObjectIDQuery(Guid AccountObjctId) : IRequest<ApiResponse<SystemUser>>;
