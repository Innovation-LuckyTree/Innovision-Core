namespace Innovision.Core.Infrastructure.Config;

public class IdentityAuthInfo
{
    public string Url { get; set; }
    public string Authority { get; set; } = "";
    public string ApiName { get; set; } = "";
    public string ClientId { get; set; } = "";
}