using Microsoft.AspNetCore.SignalR;
using PokeBattler.Client.Services;
using PokeBattler.Common.Models;
using PokeBattler.Common.Models.DTOs;
using System;
using System.Threading.Tasks;

namespace PokeBattler.Server.Services.Hubs;

public interface IHubClient
{
    Task Ping(string str);
    #region GameService
    Task CreateGame();
    #endregion
    #region ShopService
    int[] GetTierChances(int trainerLevel);
    public BuyExperienceDTO BuyExperience(Guid id);
    public BuyExperienceDTO BuyPokemon(Guid id);
    #endregion
}

public class GameHub(IGameService gameService,
                     IPokemonPoolService pokemonPoolService,
                     IShopService shopService,
                     ITrainersService trainersService
                     ) : Hub<IHubClient>
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

    #region ShopService

    public int[] GetTierChances(int trainerLevel)
    {
        var tierChances = pokemonPoolService.GetTierChances(trainerLevel);
        return tierChances;
    }

    public BuyExperienceDTO BuyExperience(Guid id)
    {
        var trainer = trainersService.GetTrainer(id);
        var dto = shopService.BuyExperience(trainer);
        return dto;
    }

    public BuyExperienceDTO BuyPokemon(Guid id)
    {
        var trainer = trainersService.GetTrainer(id);
        var dto = shopService.BuyExperience(trainer);
        return dto;
    }

    #endregion
}
