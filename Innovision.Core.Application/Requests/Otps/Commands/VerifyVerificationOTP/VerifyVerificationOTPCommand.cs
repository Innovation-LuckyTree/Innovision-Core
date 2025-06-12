using Innovision.Core.Application.Common;
using Innovision.Core.Application.Requests.Users.Queries;
using MediatR;

namespace Innovision.Core.Application.Requests.Otps.Commands.VerifyVerificationOTP;

public class VerifyVerificationOTPCommand : IRequest<ApiResponse<UnverifiedUsers>>
{
    public long ReferenceId { get; set; }
    public string MobileNumber { get; set; }
    public string OtpCode { get; set; }
}
