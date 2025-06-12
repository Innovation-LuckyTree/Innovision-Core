using Innovision.Core.Infrastructure.CoreIdentity.Models.Requests;
using Innovision.Core.Infrastructure.CoreIdentity.Models.Responses;
using Innovision.Core.Infrastructure.Helpers;
using Innovision.Core.Infrastructure.Interfaces;
using System.Net.Http.Json;

namespace Innovision.Core.Infrastructure.CoreIdentity;

public class CoreIdentityApi : AbstractApiClient, ICoreIdentityApi
{
    private readonly string _clientId;

    public CoreIdentityApi(HttpClient? client, IAppConfig appConfig) : base(nameof(CoreIdentityApi), client)
    {
        _client.BaseAddress = new Uri(appConfig.CoreIdentityApiClient.BaseAddressUrl);
        _client.DefaultRequestHeaders.Add("Resource", appConfig.CoreIdentityApiClient.Resource);

        _clientId = appConfig.AppId;
    }

    public async Task<LoginUserResponse> LoginUser(string userName, string password, string ipAddress, CancellationToken cancellationToken)
    {
        var loginRequest = new LoginUserRequest
        {
            UserName = userName,
            Password = password,
            TenantId = _clientId,
            IpAddress = ipAddress
        };

        var response = await _client.PostAsJsonAsync("api/auth/account/login", loginRequest, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadFromJsonAsync<LoginUserResponse>();
        return content!;
    }

    public async Task<CreateUserResponse> CreateUserIdentity(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PutAsJsonAsync("api/users", request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadFromJsonAsync<CreateUserResponse>();
        return content!;
    }

    public async Task UpdateUserPassword(UpdateUserPasswordRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync("api/users/password/update", request, cancellationToken);
        response.EnsureSuccessStatusCode();
    }
    public async Task<LockedUsersResponse> GetLockedUsers(Guid CompanyObjectId, int? PageNumber, int? PageSize, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"api/users/locked/list?CompanyObjectId={CompanyObjectId}&PageNumber={PageNumber}&PageSize={PageSize}", cancellationToken);
        var content = await response.Content.ReadFromJsonAsync<LockedUsersResponse>();
        return content!;
    }

    public async Task<bool> GetLockedUser(Guid UserId, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"api/users/locked?{UserId}", cancellationToken);
        var content = await response.Content.ReadFromJsonAsync<bool>();
        return content!;
    }
}