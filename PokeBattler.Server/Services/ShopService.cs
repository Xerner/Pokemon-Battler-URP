using Microsoft.Extensions.Logging;
using PokeBattler.Client.Services;
using PokeBattler.Common.Models;
using PokeBattler.Common.Models.DTOs;
using PokeBattler.Common.Models.Interfaces;
using PokeBattler.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokeBattler.Server.Services;

public interface IShopService
{
    public const bool FreePokemon = false; // TODO: app config
    public int GetCost(Pokemon pokemon);
    public void BuyPokemon(Trainer trainer, Pokemon pokemon);
    public BuyExperienceDTO BuyExperience(Trainer trainer);
    public RefreshShopDTO RefreshShop(Trainer trainer);
}
public class ShopService(ILogger<ShopService> logger, 
                         ITrainersService trainersService, 
                         IArenaService arenaService,
                         IPokemonPoolService pokemonPoolService,
                         IGameService gameService) : IShopService
{
    public const int ExperienceCost = 4;
    public const int ShopCost = 2;

    Dictionary<Guid, TrainerShop> TrainerShops;

    public int GetCost(Pokemon pokemon)
    {
        return pokemon.tier;
    }

    public BuyExperienceDTO BuyExperience(Trainer trainer)
    {
        if (!gameService.Game.GameSettings.FreeExperience)
        {
            if (trainer.Money < ExperienceCost)
            {
                logger.LogInformation("Not enough money to buy experience!");
                return new BuyExperienceDTO()
                {
                    Money = trainer.Money,
                    Level = trainer.Level,
                    Experience = trainer.Experience,
                    ExperienceNeededToLevelUp = trainersService.LevelToExpNeededToLevelUp[trainer.Level],
                    TierChances = pokemonPoolService.GetTierChances(trainer.Level)
                };
            }
            trainer.Money -= ExperienceCost;
        }
        trainersService.AddExperience(trainer, ExperienceCost);
        trainer.ExperienceNeededToLevelUp = trainersService.LevelToExpNeededToLevelUp[trainer.Level];
        return new BuyExperienceDTO()
        {
            Money = trainer.Money,
            Level = trainer.Level,
            Experience = trainer.Experience,
            ExperienceNeededToLevelUp = trainer.ExperienceNeededToLevelUp,
            TierChances = pokemonPoolService.GetTierChances(trainer.Level)
        };
    }

    public RefreshShopDTO RefreshShop(Trainer trainer)
    {
        if (!gameService.Game.GameSettings.FreeRefreshShop)
        {
            if (trainer.Money < ShopCost)
            {
                logger.LogInformation("Not enough money to refresh the shop!");
                return new RefreshShopDTO() { 
                    Money = trainer.Money,
                    ShopPokemon = null
                };
            }
            if (TrainerShops[trainer.Id].FreeShop) trainer.Money -= ShopCost;
        }
        if (TrainerShops.ContainsKey(trainer.Id))
        {
            pokemonPoolService.Refund(gameService.Game.PokemonPool, TrainerShops[trainer.Id].ShopPokemon);
        }
        var newPokemons = pokemonPoolService.Withdraw5(gameService.Game.PokemonPool, trainer.Level);
        TrainerShops[trainer.Id].ShopPokemon = newPokemons;
        return new RefreshShopDTO()
        {
            Money = trainer.Money,
            ShopPokemon = newPokemons
        }; ;
    }

    public void BuyPokemon(Trainer trainer, Pokemon pokemon)
    {
        var cost = GetCost(pokemon);
        if (trainer.Money < cost && !IShopService.FreePokemon)
        {
            logger.LogInformation("Not enough money");
            return;
        }
        IPokeContainer bench = arenaService.GetAvailableBench(trainer);
        // Trainer bought a Pokemon, but the bench is full, BUT the purchased Pokemon can be used in an evolution to free up space
        if (!BenchHasValidContainer(bench, trainer))
        {
            return;
        }
        else
        {
            bench = trainersService.GetTrainersPokemon(trainer.Id).ActivePokemon[pokemon.name][0].MoveTo;
        }
        trainer.Money -= cost;
        dashboard.Money = trainer.Money.ToString();
        logger.LogInformation($"bought a {pokemon.name} for {pokemon.tier}", LogLevel.Detailed);
        var pokemonBehaviour = await arenaService.AddPokemonToBench(pokemon);
        if (pokemonBehaviour != null)
        {
            Reset();
        }

        bool BenchHasValidContainer(IPokeContainer bench, Trainer trainer)
        {
            return bench != null || (bench == null && trainersService.GetTrainersPokemon(trainer.Id).IsAboutToEvolve(pokemon));
        }
    }
}
