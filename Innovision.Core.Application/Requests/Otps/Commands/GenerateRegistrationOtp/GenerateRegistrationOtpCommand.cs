using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Otps.Commands.GenerateRegistrationOtp
{
    public class GenerateRegistrationOtpCommand : IRequest<ApiResponse<long>>
    {
        public string MobileNumber { get; set; }
    }
}
