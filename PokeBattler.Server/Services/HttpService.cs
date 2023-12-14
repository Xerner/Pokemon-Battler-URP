using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokeBattler.Server.Services;

public class HttpService
{
    readonly ILogger<HttpService> logger;
    readonly HttpClient protocol;

    public HttpService(ILogger<HttpService> logger)
    {
        this.logger = logger;
        protocol = new HttpClient();
    }

    public string Get(string url, Action<HttpResponseMessage> onError, Action<string> onSuccess)
    {
        return Get<string>(url, onError, onSuccess);
    }

    public async Task<string> GetAsync(string url)
    {
        return await GetAsync<string>(url);
    }

    public T Get<T>(string url, Action<HttpResponseMessage> onError, Action<T> onSuccess)
    {
        logger.LogInformation("Fetching data from the web: " + url);
        Task<HttpResponseMessage> task = protocol.GetAsync(url);
        task.Wait();
        var response = task.Result;
        var processResponse = ProcessJsonResponseAsync<T>(response, onError);
        processResponse.Wait();
        var result = processResponse.Result;
        onSuccess(result);
        return result;
    }

    public async Task<T> GetAsync<T>(string url)
    {
        logger.LogInformation("Fetching string data from the web: " + url);
        var response = await protocol.GetAsync(url);
        var result = await ProcessJsonResponseAsync<T>(response);
        return result;
    }

    private async Task<T> ProcessJsonResponseAsync<T>(HttpResponseMessage response, Action<HttpResponseMessage> onError = null)
    {
        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException)
        {
            logger.LogInformation("HTTP request failed: " + response.RequestMessage.RequestUri);
            onError?.Invoke(response);
            return default;
        }
        var str = await response.Content.ReadAsStringAsync();
        logger.LogInformation("HTTP request succeeded: " + response.RequestMessage.RequestUri);
        if (typeof(T) == typeof(string))
        {
            return (T)Convert.ChangeType(str, typeof(T));
        }
        var obj = JsonConvert.DeserializeObject<T>(str);
        return obj;
    }

    private async Task<byte[]> ProcessTexture2DResponseAsync(HttpResponseMessage response, Action<HttpResponseMessage> onError = null)
    {
        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException)
        {
            logger.LogInformation("HTTP request failed: " + response.RequestMessage.RequestUri);
            onError?.Invoke(response);
            return default;
        }
        var bytes = await response.Content.ReadAsByteArrayAsync();
        logger.LogInformation("HTTP request succeeded: " + response.RequestMessage.RequestUri);
        // TODO: Move to PokeBattler.Unity
        //var texture = new Texture2D(2, 2);
        //texture.LoadImage(bytes);
        return bytes;
    }

    public byte[] GetTexture2D(string url, Action<HttpResponseMessage> onError, Action<byte[]> onSuccess)
    {
        logger.LogInformation("Fetching string data from the web: " + url);
        Task task = protocol.GetAsync(url);
        task.Wait();
        var response = protocol.GetAsync(url).Result;
        var processResponse = ProcessTexture2DResponseAsync(response, onError);
        processResponse.Wait();
        var result = processResponse.Result;
        onSuccess(result);
        return result;
    }

    public async Task<byte[]> GetTexture2DAsync(string url)
    {
        logger.LogInformation("Fetching string data from the web: " + url);
        var response = await protocol.GetAsync(url);
        var result = await ProcessTexture2DResponseAsync(response);
        return result;
    }
}
