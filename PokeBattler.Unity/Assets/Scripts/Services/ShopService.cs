using Microsoft.AspNetCore.SignalR.Client;
using PokeBattler.Common.Models;
using PokeBattler.Common.Models.DTOs;
using System;
using System.Threading.Tasks;

namespace PokeBattler.Client.Services
{
    public interface IShopService
    {
        public Task<(Guid, Pokemon)> BuyPokemon(Pokemon pokemon);
        public Task<BuyExperienceDTO> BuyExperience();
        public Task<RefreshShopDTO> RefreshShop();
    }

    public class ShopService : IShopService
    {
        readonly HubConnection connection;
        readonly IClientService clientService;
        readonly ITrainersService trainersService;

        public ShopService(HubConnection connection, IClientService clientService, ITrainersService trainersService)
        {
            this.connection = connection;
            this.clientService = clientService;
            this.trainersService = trainersService;
        }

        public async Task<(Guid, Pokemon)> BuyPokemon(Pokemon pokemon)
        {
            return connection.InvokeAsync<(Guid, Pokemon)>("BuyPokemon", (clientService.Account.Id, pokemon));
        }

        /// <summary>ActiveTrainer attempts to buy experience. Updates UI and trainer variables accordingly</summary>
        public async Task<BuyExperienceDTO> BuyExperience()
        {
            var dto = await connection.InvokeAsync<BuyExperienceDTO>("BuyExperience", trainersService.ClientsTrainer.Id);
            trainersService.ClientsTrainer.Money = dto.Money;
            trainersService.ClientsTrainer.Level = dto.Level;
            trainersService.ClientsTrainer.Experience = dto.Experience;
            trainersService.ClientsTrainer.ExperienceNeededToLevelUp = dto.ExperienceNeededToLevelUp;
            return dto;
        }

        /// <summary>ActiveTrainer attempts to refresh the entire shop. Updates UI and trainer variables accordingly</summary>
        public async Task<RefreshShopDTO> RefreshShop()
        {
            var dto = await connection.InvokeAsync<RefreshShopDTO>("RefreshShop", trainersService.ClientsTrainer.Id);
            trainersService.ClientsTrainer.Money = dto.Money;
            return dto;
        }
    }
}