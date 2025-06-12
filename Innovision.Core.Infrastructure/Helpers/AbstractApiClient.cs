using System.Net.Http.Json;

namespace Innovision.Core.Infrastructure.Helpers;

public abstract class AbstractApiClient
{
    /// <summary>
    /// The Name of the API.
    /// </summary>
    protected readonly string _apiName;

    /// <summary>
    /// The HTTP Client.
    /// </summary>
    protected readonly HttpClient _client;

    /// <summary>
    /// Creates an <see cref="AbstractApiClient"/>.
    /// </summary>
    /// <param name="client">The HTTP Client.</param>
    /// <exception cref="ArgumentNullException">Thrown if any parameter is null.</exception>
    public AbstractApiClient(string apiName, HttpClient? client)
    {
        _apiName = apiName;
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    /// <summary>
    /// Calls GET on the URI.
    /// Binds response to the given Response type.
    /// </summary>
    /// <typeparam name="Response">The Response model.</typeparam>
    /// <param name="requestUri">The URI to call GET on.</param>
    /// <exception cref="HttpRequestException">Thrown if the status code is something other than 2XX.</exception>
    protected virtual async Task<Response> GetAsync<Response>(string requestUri)
    {
        var response = await _client.GetAsync(requestUri);
        await response.EnsureApiSuccessAsync(_apiName);
        var content = await response.Content.ReadFromJsonAsync<Response>();
        return content!;
    }

    /// <summary>
    /// POSTs the Request model as JSON to the URI.
    /// </summary>
    /// <typeparam name="Request">The Request model.</typeparam>
    /// <param name="requestUri">The URI to call POST on.</param>
    /// <param name="request">The Request model.</param>
    /// <exception cref="HttpRequestException">Thrown if the status code is something other than 2XX.</exception>
    protected virtual async Task PostAsync<Request>(string requestUri, Request request)
    {
        var response = await _client.PostAsJsonAsync(requestUri, request);
        await response.EnsureApiSuccessAsync(_apiName);
    }

    /// <summary>
    /// POSTs the Request model as JSON to the URI.
    /// Binds response to the given Response type.
    /// </summary>
    /// <typeparam name="Request">The Request model.</typeparam>
    /// <typeparam name="Response">The Response model.</typeparam>
    /// <param name="requestUri">The URI to call POST on.</param>
    /// <param name="request">The Request model.</param>
    /// <exception cref="HttpRequestException">Thrown if the status code is something other than 2XX.</exception>
    protected virtual async Task<Response> PostAsync<Request, Response>(string requestUri, Request request)
    {
        var response = await _client.PostAsJsonAsync(requestUri, request);
        await response.EnsureApiSuccessAsync(_apiName);
        var content = await response.Content.ReadFromJsonAsync<Response>();
        return content!;
    }

    /// <summary>
    /// POSTs the HTTP Content to the URI.
    /// Binds response to the given Response type.
    /// </summary>
    /// <typeparam name="Response">The Response model.</typeparam>
    /// <param name="requestUri">The URI to call POST on.</param>
    /// <param name="content"></param>
    /// <exception cref="ApiException">Thrown if the status code is something other than 2XX.</exception>
    protected virtual async Task<Response> PostAsync<Response>(string requestUri, HttpContent content)
    {
        var response = await _client.PostAsync(requestUri, content);
        await response.EnsureApiSuccessAsync(_apiName);
        var respContent = await response.Content.ReadFromJsonAsync<Response>();
        return respContent!;
    }

    /// <summary>
    /// PUTs the Request model as JSON to the URI.
    /// Binds response to the given Response type.
    /// </summary>
    /// <typeparam name="Request">The Request model.</typeparam>
    /// <typeparam name="Response">The Response model.</typeparam>
    /// <param name="requestUri">The URI to call PUT on.</param>
    /// <param name="request">The Request model.</param>
    /// <exception cref="HttpRequestException">Thrown if the status code is something other than 2XX.</exception>
    protected virtual async Task<Response> PutAsync<Request, Response>(string requestUri, Request request)
    {
        var response = await _client.PutAsJsonAsync(requestUri, request);
        await response.EnsureApiSuccessAsync(_apiName);
        var content = await response.Content.ReadFromJsonAsync<Response>();
        return content!;
    }
}