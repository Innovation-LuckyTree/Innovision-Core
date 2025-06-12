using Innovision.Core.Infrastructure.Models;

public static class HttpResponseMessageExtensions
{
    public static async Task<HttpResponseMessage> EnsureApiSuccessAsync(this HttpResponseMessage response, string apiName)
    {
        try
        {
            return response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException hre)
        {
            string body = await response.Content.ReadAsStringAsync();
            throw new ApiException(apiName, body, hre.Message, hre.InnerException, hre.StatusCode);
        }
    }
}