using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Json;

namespace MainTest.Framework.HttpClientHelper
{
    public class HttpClientHelper : IHttpClientHelper
    {
        public async Task<TResponse> GetAsync<TResponse>(HttpClient client, string requestUri, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
            if (parameters != null)
            {
                requestUri = QueryHelpers.AddQueryString(requestUri, parameters);
            }
            var response = await client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            // ReSharper disable once UnusedVariable
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TResponse>(result);
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest request, HttpClient client, string requestUri, Dictionary<string, string> headers = null,
            JsonSerializerOptions options = null)
        {
            try
            {
                var requestJson = JsonSerializer.Serialize(request, typeof(TRequest), options);
                var requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");

                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

                var response = await client.PostAsync(requestUri, requestContent);

                response.EnsureSuccessStatusCode();

                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<TResponse>(result);
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> PostAsync<TRequest>(TRequest request, HttpClient client, string requestUri, Dictionary<string, string> headers = null, JsonSerializerOptions options = null)
        {
            try
            {
                var requestJson = JsonSerializer.Serialize(request, typeof(TRequest), options);
                var requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");

                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

                var response = await client.PostAsync(requestUri, requestContent);

                response.EnsureSuccessStatusCode();
                return true;
            }
            catch(HttpRequestException ex)
            { 
                return false;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}

