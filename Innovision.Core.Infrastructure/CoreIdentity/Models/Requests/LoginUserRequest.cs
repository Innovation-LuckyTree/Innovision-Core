namespace Innovision.Core.Infrastructure.CoreIdentity.Models.Requests;

public class LoginUserRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string TenantId { get; set; }
    public string IpAddress { get; set; }
}