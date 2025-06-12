using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.Commands.UpdateUserStatuses;
public class UpdateUserStatusesCommand : IRequest<ApiResponse<bool>>
{
    public long AccountInfoId { get; set; }
    public bool? IsActive { get; set; }
    public int? Status { get; set; }
    public int? SubStatus { get; set; }
    public string Remarks { get; set; }
}
