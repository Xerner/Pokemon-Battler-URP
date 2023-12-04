using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PokeBattler.Common.Models;
using PokeBattler.Server.Services;

namespace PokeBattler.Server.Controllers;

public class TrainersController(HubService hub, ITrainerService trainerService, IAccountService accountService)
{
    readonly ITrainerService trainerService = trainerService;

    public async Task Test(string str)
    {
        await hub.Clients.All.SendAsync("Test", str);
    }

    public async Task UpdateTrainerReady(Guid id, bool ready)
    {
        await hub.Clients.All.SendAsync("UpdateTrainerReady", id, ready);
    }

    public async Task AddToGame(Account account)
    {
        account = trainerService.Create(account);
        await hub.Clients.All.SendAsync("AddToGame", account);
    }
}
