using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokeBattler.Server.Services;

public class HttpService
{
    readonly HttpClient protocol;
    public HttpService()
    {
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
        //Debug2.Log("Fetching data from the web: " + url, LogLevel.All);
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
        //Debug2.Log("Fetching string data from the web: " + url, LogLevel.All);
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
            //Debug2.Log("HTTP request failed: " + response.RequestMessage.RequestUri, LogLevel.All);
            onError?.Invoke(response);
            return default;
        }
        var str = await response.Content.ReadAsStringAsync();
        //Debug2.Log("HTTP request succeeded: " + response.RequestMessage.RequestUri, LogLevel.All);
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
            //Debug2.Log("HTTP request failed: " + response.RequestMessage.RequestUri, LogLevel.All);
            onError?.Invoke(response);
            return default;
        }
        var bytes = await response.Content.ReadAsByteArrayAsync();
        //Debug2.Log("HTTP request succeeded: " + response.RequestMessage.RequestUri, LogLevel.All);
        //var texture = new Texture2D(2, 2);
        //texture.LoadImage(bytes);
        return bytes;
    }

    public byte[] GetTexture2D(string url, Action<HttpResponseMessage> onError, Action<byte[]> onSuccess)
    {
        //Debug2.Log("Fetching string data from the web: " + url, LogLevel.All);
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
        //Debug2.Log("Fetching string data from the web: " + url, LogLevel.All);
        var response = await protocol.GetAsync(url);
        var result = await ProcessTexture2DResponseAsync(response);
        return result;
    }
}
