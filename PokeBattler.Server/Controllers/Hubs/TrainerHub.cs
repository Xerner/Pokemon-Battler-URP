using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace PokeBattler.Server.Controllers.Hubs
{
    public class TrainerHub : Hub
    {
        public async Task UpdateTrainerReady(ulong id, bool ready)
        {
            await Clients.All.SendAsync("UpdateTrainerReady", id, ready);
        }
    }
}
