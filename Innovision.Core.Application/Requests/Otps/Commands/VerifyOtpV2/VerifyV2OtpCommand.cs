using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Otps.Commands.VerifyOtp;

public class VerifyV2OtpCommand : IRequest<ApiResponse<bool>>
{
    public long ReferenceId { get; set; }
    public string MobileNumber { get; set; }
    public string OtpCode { get; set; }
}
