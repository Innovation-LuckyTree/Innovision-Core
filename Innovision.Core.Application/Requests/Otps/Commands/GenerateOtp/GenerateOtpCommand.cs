using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Otps.Commands.GenerateOtp;

public class GenerateOtpCommand : IRequest<ApiResponse<long>>
{
    public string MobileNumber { get; set; }
}
