using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.Commands.UpdateProfession;

public class UpdateProfessionCommand : IRequest<ApiResponse<bool>>
{
    public string SourceOfIncome { get; set; }
    public string NatureOfWork { get; set; }
    public int? SalaryRange { get; set; }
}
