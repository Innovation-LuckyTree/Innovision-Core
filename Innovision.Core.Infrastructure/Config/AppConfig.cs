using Innovision.Core.Infrastructure.Interfaces;

namespace Innovision.Core.Infrastructure.Config;

public class AppConfig : IAppConfig
{
    public string AppId { get; set; }
    public string MobileAppId { get; set; }
    public JwtConfig JwtConfig { get; set; }
    public IdentityAuthInfo IdentityAuthInfo { get; set; }
    public ApiClientConfig CoreIdentityApiClient { get; set; }
    public ApiClientConfig GameApiClient { get; set; }
    public ApiClientConfig AccountServiceApiClient { get; set; }
    public ApiClientConfig SupportApiClient { get; set; }
    public ApiClientConfig PaymentApiClient { get; set; }
    public ApiClientConfig WebsocketApiClient { get; set; }
    public ApiClientConfig MessageBrokerClient { get; set; }
    public string UploadPath { get; set; }
    public decimal GMFCommission { get; set; }
}
