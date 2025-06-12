using System.Security.Authentication;

namespace Innovision.Core.Infrastructure.Helpers;

public class PrimaryHttpClientHandlerFactory
{
    public static HttpClientHandler CreateHttpClientHandler() => new HttpClientHandler { SslProtocols = SslProtocols.Tls12 };
}
