namespace Innovision.Core.Application.Common.OTP
{
    public class GenerateOTPResponse
    {
        public long ReferenceId { get; set; }
        public Guid UserId { get; set; }
        public bool New { get; set; }
    }
}
