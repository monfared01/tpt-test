using System.Text.Json;

namespace MainTest.Framework.HttpClientHelper
{
    public interface IHttpClientHelper
    {
        Task<TResponse> GetAsync<TResponse>(HttpClient client, string requestUri, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null);

        Task<TResponse> PostAsync<TRequest, TResponse>(TRequest request, HttpClient client, string requestUri,
            Dictionary<string, string> headers = null, JsonSerializerOptions options = null);

        Task<bool> PostAsync<TRequest>(TRequest request, HttpClient client, string requestUri, Dictionary<string, string> headers = null, JsonSerializerOptions options = null);
    }
}
