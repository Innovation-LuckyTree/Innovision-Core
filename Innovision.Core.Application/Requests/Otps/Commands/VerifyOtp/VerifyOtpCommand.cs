using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Otps.Commands.VerifyOtp;

public class VerifyOtpCommand : IRequest<ApiResponse<bool>>
{
    public string MobileNumber { get; set; }
    public string OtpCode { get; set; }
}
