namespace Innovision.Core.Domain.Entity;

public class OTP
{
    public long OtpID { get; set; }
    public string MobileNumber { get; set; }
    public string Code { get; set; }
    public bool IsVerify { get; set; }
    public string TransType { get; set; }
    public  DateTimeOffset CreatedOn { get; set; }
    public  DateTimeOffset ExpireDate { get; set; }
}

