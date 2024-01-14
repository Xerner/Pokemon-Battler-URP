using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using PokeBattler.Client.Models;
using PokeBattler.Common.Models;
using PokeBattler.Common.Models.DTOs;

namespace PokeBattler.Client.Services
{
    public interface IShopService
    {
        public Action<RefreshShopDTO> OnShopRefreshed { get; set; }
        public Action<BuyPokemonDTO> OnPokemonBought { get; set; }
        public Task RequestToBuyPokemon(int shopIndex, Pokemon pokemon);
        public void PokemonBought(BuyPokemonDTO pokemon);
        public Task<BuyExperienceDTO> BuyExperience();
        public Task RefreshShop();
    }

    public class ShopService : IShopService
    {
        public Action<RefreshShopDTO> OnShopRefreshed { get; set; }
        public Action<BuyPokemonDTO> OnPokemonBought { get; set; }

        readonly HubConnection connection;
        readonly IClientService clientService;
        readonly ITrainersService trainersService;

        public ShopService(HubConnection connection, 
                           IClientService clientService, 
                           ITrainersService trainersService)
        {
            this.connection = connection;
            this.clientService = clientService;
            this.trainersService = trainersService;
            connection.On<BuyPokemonDTO>(nameof(HubClient.Singleton.PokemonBought), PokemonBought);
        }

        public async Task RequestToBuyPokemon(int shopIndex, Pokemon pokemon)
        {
            await connection.InvokeAsync(nameof(HubServer.Singleton.BuyPokemon), (clientService.Account.Id, shopIndex, pokemon));
        }

        public void PokemonBought(BuyPokemonDTO dto)
        {
            OnPokemonBought.Invoke(dto);
        }

        /// <summary>ActiveTrainer attempts to buy experience. Updates UI and trainer variables accordingly</summary>
        public async Task<BuyExperienceDTO> BuyExperience()
        {
            var dto = await connection.InvokeAsync<BuyExperienceDTO>(nameof(HubServer.Singleton.BuyExperience), trainersService.ClientsTrainer.Id);
            trainersService.ClientsTrainer.Money = dto.Money;
            trainersService.ClientsTrainer.Level = dto.Level;
            trainersService.ClientsTrainer.Experience = dto.Experience;
            trainersService.ClientsTrainer.ExperienceNeededToLevelUp = dto.ExperienceNeededToLevelUp;
            return dto;
        }

        /// <summary>ActiveTrainer attempts to refresh the entire shop. Updates UI and trainer variables accordingly</summary>
        public async Task RefreshShop()
        {
            var dto = await connection.InvokeAsync<RefreshShopDTO>(nameof(HubServer.Singleton.RefreshShop), trainersService.ClientsTrainer.Id);
            trainersService.ClientsTrainer.Money = dto.Money;
            OnShopRefreshed.Invoke(dto);
        }
    }
}