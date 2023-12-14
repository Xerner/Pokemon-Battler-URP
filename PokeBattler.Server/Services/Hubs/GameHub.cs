using Microsoft.AspNetCore.SignalR;
using PokeBattler.Client.Services;
using PokeBattler.Common.Models;
using PokeBattler.Common.Models.DTOs;
using PokeBattler.Common.Models.Interfaces;
using System;
using System.Threading.Tasks;

namespace PokeBattler.Server.Services.Hubs;

public class GameHub(IGameService gameService,
                     IPokemonPoolService pokemonPoolService,
                     IShopService shopService,
                     ITrainersService trainersService
                     ) : Hub<IHubClient>, IHubServer
{
    public const string HubName = "Games";

    public string Ping(string str)
    {
        return str;
    }

    #region GameService

    public Game CreateGame()
    {
        var game = gameService.CreateGame();
        return game;
    }

    #endregion

    #region

    public void AddToGame(Account account)
    {
        var trainer = trainersService.Create(account);
        Clients.All.AddTrainerToGame(trainer);
    }

    #endregion

    #region ShopService

    public int[] GetTierChances(int trainerLevel)
    {
        var tierChances = pokemonPoolService.GetTierChances(trainerLevel);
        return tierChances;
    }

    public RefreshShopDTO RefreshShop(Guid id)
    {
        var trainer = trainersService.GetTrainer(id);
        var dto = shopService.RefreshShop(trainer);
        return dto;
    }

    public BuyExperienceDTO BuyExperience(Guid id)
    {
        var trainer = trainersService.GetTrainer(id);
        var level = trainer.Level;
        var dto = shopService.BuyExperience(trainer);
        // TODO: implement this in the client
        if (trainer.Level == level)
        {
            var trainerLevelUpDTO = new TrainerLevelUpDTO()
            {
                Id = id,
                Level = level,
            };
            Clients.All.TrainerLevelUp(trainerLevelUpDTO);
        }
        return dto;
    }
    public async Task BuyPokemon(Guid id, int shopIndex, int pokemonId)
    {
        var trainer = trainersService.GetTrainer(id);
        var dto = await shopService.BuyPokemon(trainer, shopIndex, pokemonId);
        await Clients.All.PokemonBought(dto);
    }

    #endregion
}
