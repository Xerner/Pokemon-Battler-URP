using System;
using System.Threading.Tasks;
using PokeBattler.Common.Models;
using PokeBattler.Common.Models.DTOs;
using PokeBattler.Common.Models.Interfaces;

namespace PokeBattler.Client.Models
{
    public class HubServer : IHubServer
    {
        public static HubServer Singleton = new();

        public Task AddToGame(Account account)
        {
            throw new NotImplementedException();
        }

        public BuyExperienceDTO BuyExperience(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task BuyPokemon(Guid id, int shopIndex, int pokemonId)
        {
            throw new NotImplementedException();
        }

        public Game CreateGame()
        {
            throw new NotImplementedException();
        }

        public int[] GetTierChances(int trainerLevel)
        {
            throw new NotImplementedException();
        }

        public string Ping(string str)
        {
            throw new NotImplementedException();
        }

        public RefreshShopDTO RefreshShop(Guid id)
        {
            throw new NotImplementedException();
        }

        void IHubServer.AddToGame(Account account)
        {
            throw new NotImplementedException();
        }
    }
}