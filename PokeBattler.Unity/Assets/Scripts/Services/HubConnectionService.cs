using Microsoft.AspNetCore.SignalR.Client;

namespace PokeBattler.Client.Services
{
    public class HubConnectionService
    {
        public const string HubName = "Games";
        public const string HubUrl = "https://localhost:5001/" + HubName;

        public static readonly HubConnection Connection = new HubConnectionBuilder()
            .WithUrl(HubUrl)
            .WithAutomaticReconnect()
            .Build();

        public HubConnectionService()
        {
            Connection.StartAsync();
        }
    }
}
