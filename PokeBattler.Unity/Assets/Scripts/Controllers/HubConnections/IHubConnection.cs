using Microsoft.AspNetCore.SignalR.Client;

namespace PokeBattler.Client.Controllers.HubConnections
{
    public interface IHubConnection
    {
        HubConnection HubConnection { get; }
    }
}
