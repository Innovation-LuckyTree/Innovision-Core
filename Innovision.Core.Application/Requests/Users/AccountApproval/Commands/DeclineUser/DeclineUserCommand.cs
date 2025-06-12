using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.AccountApproval.Commands;

public record DeclineUserCommand(Guid AccountInfoId, string Remarks) : IRequest<ApiResponse<bool>>;
