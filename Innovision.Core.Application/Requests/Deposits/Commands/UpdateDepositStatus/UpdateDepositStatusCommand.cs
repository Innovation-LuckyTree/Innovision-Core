using Innovision.Core.Application.Requests.Deposits.Queries;
using MediatR;

namespace Innovision.Core.Application.Requests.Deposits.Commands.AddUserDepositRequest;

public class UpdateDepositStatusCommand : IRequest<DepositDto>
{
    public long DepositId { get; set; }
    public int Status { get; set; }
    public string Remarks { get; set; }
}
