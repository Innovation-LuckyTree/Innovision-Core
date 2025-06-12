using MediatR;

namespace Innovision.Core.Application.Requests.Otps.Commands.CreateOtp;

public record CreateOtpCommand(string MobileNumber) : IRequest<long>
{
    public int MessageType { get; set; }
}
