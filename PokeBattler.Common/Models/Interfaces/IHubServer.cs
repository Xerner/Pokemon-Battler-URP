using System;
using System.Threading.Tasks;
using PokeBattler.Common.Models.DTOs;

namespace PokeBattler.Common.Models.Interfaces
{
    public interface IHubServer
    {
        public string Ping(string str);
        public Game CreateGame();
        public void AddToGame(Account account);
        public int[] GetTierChances(int trainerLevel);
        public BuyExperienceDTO BuyExperience(Guid id);
        public Task BuyPokemon(Guid id, int shopIndex, int pokemonId);
        public RefreshShopDTO RefreshShop(Guid id);
    }
}
