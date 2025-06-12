using Innovision.Core.Infrastructure.Interfaces;
using Innovision.Core.Common.Interfaces;

namespace Innovision.Core.Infrastructure.Config;

public class IdentityBearerTokenHandler : DelegatingHandler
{
    /// <summary>
    /// The Identity API Client to retrieve the Auth token from.
    /// </summary>
    private readonly ICurrentUserService _currentUserService;
    private readonly IAppConfig _appConfig;

    /// <summary>
    /// Creates an <see cref="IdentityBearerTokenHandler"/> object.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown when any argument is null.</exception>
    public IdentityBearerTokenHandler(IAppConfig appConfig, ICurrentUserService currentUserService)
    {
        _appConfig = appConfig;
        _currentUserService = currentUserService;

    }

    /// <summary>
    /// If the Authorization header is missing, will call the Identity API and retrieve an auth token.
    /// Adds the Authorization header and then continues the HTTP Request.
    /// </summary>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (!request.Headers.Contains("Authorization"))
        {
            request.Headers.Add("Authorization", $"Bearer {_currentUserService.AuthenticationBearer}");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("odata", "verbose");
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
