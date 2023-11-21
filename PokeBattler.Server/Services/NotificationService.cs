using Microsoft.AspNetCore.SignalR;
using PokeBattler.Server.Services.Hubs;
using PokeServer.Models;

namespace PokeBattler.Server.Services;

public sealed class NotificationService
{
    private readonly IHubContext<TrainerHub> hubContext;

    public NotificationService(IHubContext<TrainerHub> hubContext)
    {
        this.hubContext = hubContext;
    }

    public Task SendNotificationAsync(Notification notification) =>
        notification is not null
            ? hubContext.Clients.All.SendAsync("NotificationReceived", notification)
            : Task.CompletedTask;
}
