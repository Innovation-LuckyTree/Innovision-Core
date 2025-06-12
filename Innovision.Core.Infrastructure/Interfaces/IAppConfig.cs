
using Innovision.Core.Infrastructure.Config;

namespace Innovision.Core.Infrastructure.Interfaces;

public interface IAppConfig
{
    string AppId { get; set; }
    string MobileAppId { get; set; }
    JwtConfig JwtConfig { get; set; }
    IdentityAuthInfo IdentityAuthInfo { get; set; }
    ApiClientConfig CoreIdentityApiClient { get; set; }
    ApiClientConfig GameApiClient { get; set; }
    ApiClientConfig SupportApiClient { get; set; }
    ApiClientConfig AccountServiceApiClient { get; set; }
    ApiClientConfig PaymentApiClient { get; set; }
    ApiClientConfig WebsocketApiClient { get; set; }
    ApiClientConfig MessageBrokerClient { get; set; }
    string UploadPath { get; set; }
    decimal GMFCommission { get; set; }
}