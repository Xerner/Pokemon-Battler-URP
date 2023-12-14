using System.Threading.Tasks;
using PokeBattler.Common.Models.DTOs;

namespace PokeBattler.Common.Models.Interfaces
{
    public interface IHubClient
    {
        public string Ping(string str);
        public Task AddTrainerToGame(Trainer trainer);
        #region ShopService
        public Task TrainerLevelUp(TrainerLevelUpDTO dto);
        public Task PokemonBought(BuyPokemonDTO dto);
        #endregion
    }
}
