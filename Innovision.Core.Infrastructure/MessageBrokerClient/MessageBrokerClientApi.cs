using Innovision.Core.Infrastructure.Interfaces;
using Innovision.Core.Infrastructure.Helpers;
using System.Net.Http.Json;
using Innovision.Core.Infrastructure.MessageBrokerClient.Models;

namespace Innovision.Core.Infrastructure.MessageBrokerClient;

public class MessageBrokerClientApi : AbstractApiClient, IMessageBrokerClientApi
{
    public MessageBrokerClientApi(HttpClient? client, IAppConfig appConfig) : base(nameof(MessageBrokerClientApi), client)
    {
        _client.BaseAddress = new Uri(appConfig.MessageBrokerClient.BaseAddressUrl);
        _client.DefaultRequestHeaders.Add("Resource", appConfig.MessageBrokerClient.Resource);
    }

    public async Task<CreateRecordResponse> AddRecordAsync<T>(CreateRecordRequest<T> request, CancellationToken cancellationToken) where T: class
    {
        var response = await _client.PostAsJsonAsync($"/kafka/send-task", request, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadFromJsonAsync<CreateRecordResponse>(cancellationToken);
        return content;
    }
}
