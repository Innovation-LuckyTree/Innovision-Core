using Innovision.Core.Application.Common;
using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.Accounts.Users.Operator.Commands;

public class CreateOperatorCommand : IRequest<ApiResponse<Unit>>
{
    public int BranchId { get; set; }
    public Details Details { get; set; }
}

