namespace Innovision.Core.Application.Requests.Otps.Commands.GenerateLoginOtp;

public class LoginOtpDto
{
    public long ReferenceId { get; set; }
    public Guid UserId { get; set; }
    public bool New { get; set; }
}
