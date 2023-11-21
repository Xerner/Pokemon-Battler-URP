using Microsoft.AspNetCore.SignalR;
using PokeServer.Models;

namespace PokeBattler.Server.Services.Hubs;

public sealed class TrainerHub : Hub
{
    public Task NotifyAll(Notification notification)
    {
        return Clients.All.SendAsync("NotificationReceived", notification);
    }
}
