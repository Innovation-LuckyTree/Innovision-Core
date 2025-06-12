using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.Queries.GetSystemUserById;

public record GetSystemUserByIdQuery(Guid UserId) : IRequest<ApiResponse<SystemUser>>;
