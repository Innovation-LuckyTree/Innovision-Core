using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.AccountApproval.Commands;

public class DeclinedUserVerificationCommand : IRequest<ApiResponse<bool>>
{
    public Guid AccountObjectId { get; set; }
    public string Remarks { get; set; }
}
