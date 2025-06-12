using Innovision.Core.Application.Common;
using Innovision.Core.Infrastructure.CoreIdentity.Models.Responses;
using MediatR;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetUserToken;

public class GetUserTokenCommand : IRequest<ApiResponse<LoginResponse>>
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string IpAddress { get; set; }
}
