using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.AccountApproval.Commands;

public record ApproveUserVerificationCommand (Guid AccountObjId) : IRequest<ApiResponse<bool>>;
