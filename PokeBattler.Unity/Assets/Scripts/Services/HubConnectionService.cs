using Microsoft.AspNetCore.SignalR.Client;

namespace PokeBattler.Client.Services
{
    //public interface IServerConnection
    //{
    //    public const string HubUrl = "https://localhost:5000/Hubs";
    //    Task SendAsync(string serverMethodName);
    //    Task SendAsync(string serverMethodName, object arg0);
    //    Task SendAsync(string serverMethodName, object arg0, object arg1);
    //    IDisposable On(string serverMethodName, Func<Task> handler);
    //    IDisposable On<T>(string serverMethodName, Func<T, Task> handler);
    //    IDisposable On<T>(string serverMethodName, Func<T, Task<T>> handler);
    //}

    public class HubConnectionService
    {
        public const string HubName = "Games";
        public const string HubUrl = "https://localhost:5001/" + HubName;
        //public HubConnection Connection { get; private set; }

        public static readonly HubConnection Connection = new HubConnectionBuilder()
            .WithUrl(HubUrl)
            .WithAutomaticReconnect()
            .Build();

        public HubConnectionService()
        {
            Connection.StartAsync();
        }

        //public HubConnectionService()
        //{
        //    Connection = new HubConnectionBuilder()
        //        .WithUrl(HubUrl)
        //        .WithAutomaticReconnect()
        //        .Build();
        //}

        //public Task SendAsync(string serverMethodName)
        //{
        //    return Connection.SendAsync(serverMethodName);
        //}

        //public Task SendAsync(string serverMethodName, object arg0)
        //{
        //    return Connection.InvokeAsync(serverMethodName, arg0);
        //}

        //public Task SendAsync(string serverMethodName, object arg0, object arg1)
        //{
        //    return Connection.InvokeAsync(serverMethodName, arg0, arg1);
        //}

        //public IDisposable On(string serverMethodName, Func<Task> handler)
        //{
        //    return Connection.On(serverMethodName, handler);
        //}

        //public IDisposable On<T>(string serverMethodName, Func<T, Task> handler)
        //{
        //    return Connection.On(serverMethodName, handler);
        //}

        //public IDisposable On<T>(string serverMethodName, Func<T, Task<T>> handler)
        //{
        //    return Connection.On(serverMethodName, handler);
        //}
    }
}
