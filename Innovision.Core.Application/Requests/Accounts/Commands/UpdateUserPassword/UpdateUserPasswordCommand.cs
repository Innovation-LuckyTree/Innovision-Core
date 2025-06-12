using MediatR;

namespace Innovision.Core.Application.Requests.Accounts.Commands.UpdateUserPassword;

public class UpdateUserPasswordCommand : IRequest<Unit>
{
    public Guid UserId { get; set; }
    public string MobileNumber { get; set; }
    public long OtpReferenceId { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}
