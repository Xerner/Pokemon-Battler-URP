using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokeBattler.Client.Services
{
    public interface IPokemonPoolService
    {
        public Task<IEnumerable<int>> GetTierChances(int trainerLevel);
    }

    public class PokemonPoolService : IPokemonPoolService
    {
        readonly HubConnection connection;
        IEnumerable<int> cachedTierChances = null;

        public PokemonPoolService(HubConnection connection)
        {
            this.connection = connection;
        }

        public async Task<IEnumerable<int>> GetTierChances(int trainerLevel)
        {
            if (connection.State == HubConnectionState.Disconnected) return cachedTierChances;
            cachedTierChances = await connection.InvokeAsync<int[]>("GetTierChances", trainerLevel);
            return cachedTierChances;
        }
    }
}