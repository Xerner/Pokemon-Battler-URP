using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace PokeBattler.Client.Factories
{
    public interface IHubConnectionFactory
    {
        HubConnection Create(string HubName);
    }

    public class HubConnectionFactory : IHubConnectionFactory
    {
        private static readonly string ApiPath = "https://localhost:5387/Hubs";

        public HubConnection Create(string HubName)
        {
            var hubConnection = new HubConnectionBuilder()
                .WithUrl(new Uri($"{ApiPath}/{HubName}"))
                .WithAutomaticReconnect()
                .Build();

            return hubConnection;
        }
    }
}
