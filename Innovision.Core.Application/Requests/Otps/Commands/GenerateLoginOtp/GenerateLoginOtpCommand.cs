using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Otps.Commands.GenerateLoginOtp;

public class GenerateLoginOtpCommand : IRequest<ApiResponse<LoginOtpDto>>
{
    public string MobileNumber { get; set; }
}
