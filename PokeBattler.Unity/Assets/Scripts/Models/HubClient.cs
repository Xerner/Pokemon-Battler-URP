using System;
using System.Threading.Tasks;
using PokeBattler.Common.Models.DTOs;
using PokeBattler.Common.Models.Interfaces;
using PokeBattler.Common.Models;

namespace PokeBattler.Client.Models
{
    public class HubClient : IHubClient
    {
        public static HubClient Singleton = new();

        public Task AddTrainerToGame(Trainer trainer)
        {
            throw new NotImplementedException();
        }

        public string Ping(string str)
        {
            throw new NotImplementedException();
        }

        public Task PokemonBought(BuyPokemonDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task TrainerLevelUp(TrainerLevelUpDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}