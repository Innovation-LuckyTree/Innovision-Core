using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Otps.Commands.GenerateVerificationOtp
{
    public class GenerateVerificationOtpCommand : IRequest<ApiResponse<long>>
    {
        public string MobileNumber { get; set; }
    }
}
