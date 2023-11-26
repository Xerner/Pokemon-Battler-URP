using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace PokeBattler.Server.Services.Hubs
{
    public class TrainerHub : Hub
    {
        public async Task UpdateTrainerReady(bool ready)
        {
            await Clients.All.SendAsync("UpdateTrainerReady", ready);
            return;
        }
    }
}
