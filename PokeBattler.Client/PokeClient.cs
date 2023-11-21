using Microsoft.AspNetCore.SignalR.Client;

namespace PokeBattler.Client
{
    public class PokeClient
    {
        private readonly string HostDomain = "https://localhost:5387";

        private HubConnection _hubConnection;

        public PokeClient()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(new Uri($"{HostDomain}/hub/notifications"))
                .WithAutomaticReconnect()
                .Build();
        }

        public Task StartNotificationConnectionAsync() => _hubConnection.StartAsync();

        public async ValueTask DisposeAsync()
        {
            if (_hubConnection is not null)
            {
                await _hubConnection.DisposeAsync();
                _hubConnection = null;
            }
        }
    }
}