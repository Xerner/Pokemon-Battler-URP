namespace AutoChess.Infrastructure.Interfaces;

public interface IHttpService
{
    string Get(string url, Action<HttpResponseMessage> onError, Action<string> onSuccess);
    T Get<T>(string url, Action<HttpResponseMessage> onError, Action<T> onSuccess);
    Task<string> GetAsync(string url);
    Task<T> GetAsync<T>(string url);
    byte[] GetTexture2D(string url, Action<HttpResponseMessage> onError, Action<byte[]> onSuccess);
    Task<byte[]> GetTexture2DAsync(string url);
}
