using Microsoft.AspNetCore.SignalR;

namespace PokeBattler.Server.Services;

public class HubService : Hub
{
    public const string HubName = "Games";
}
