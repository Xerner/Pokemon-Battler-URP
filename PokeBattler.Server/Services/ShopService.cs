using Microsoft.Extensions.Logging;
using PokeBattler.Client.Services;
using PokeBattler.Common.Models;
using PokeBattler.Common.Models.DTOs;
using PokeBattler.Common.Models.Enums;
using PokeBattler.Server.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokeBattler.Server.Services;

public interface IShopService
{
    public const bool FreePokemon = false; // TODO: app config
    public int GetCost(Pokemon pokemon);
    public Task<BuyPokemonDTO> BuyPokemon(Trainer trainer, int shopIndex, int pokemonId);
    public BuyExperienceDTO BuyExperience(Trainer trainer);
    public RefreshShopDTO RefreshShop(Trainer trainer);
}

public class ShopService(ILogger<ShopService> logger, 
                         ITrainersService trainersService, 
                         IArenaService arenaService,
                         IPokemonPoolService pokemonPoolService,
                         IPokeApiService pokemonApiService,
                         IGameService gameService) : IShopService
{
    public const int ExperienceCost = 4;
    public const int ShopCost = 2;

    readonly Dictionary<Guid, TrainerShop> TrainerShops = [];

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
        var shop = TrainerShops[trainer.Id];
        if (!gameService.Game.GameSettings.FreeRefreshShop)
        {
            if (trainer.Money < ShopCost)
            {
                logger.LogInformation("Not enough money to refresh the shop!");
                return new RefreshShopDTO() { 
                    Money = trainer.Money,
                    Shop = null
                };
            }
            if (shop.FreeShop)
            {
                trainer.Money -= ShopCost;
            }
        }
        pokemonPoolService.Refund(gameService.Game.PokemonPool, shop.ShopPokemon);
        var newPokemons = pokemonPoolService.Withdraw5(gameService.Game.PokemonPool, trainer.Level);
        shop.ShopPokemon = newPokemons;
        return new RefreshShopDTO()
        {
            Money = trainer.Money,
            Shop = shop
        };
    }

    public async Task<BuyPokemonDTO> BuyPokemon(Trainer trainer, int shopIndex, int pokemonId)
    {
        var shop = TrainerShops[trainer.Id];
        var pokemon = await pokemonApiService.GetPokemon(pokemonId);
        var cost = GetCost(pokemon);
        if (trainer.Money < cost && !IShopService.FreePokemon)
        {
            logger.LogInformation("Not enough money");
            return null;
        }
        pokemon.Id = new Guid();
        var containerType = EContainerType.Bench;
        var benchIndex = arenaService.GetAvailableBenchIndex(trainer);
        var isAboutToEvolve = trainersService.TrainersPokemon[trainer.Id].IsAboutToEvolve(pokemon);
        if (benchIndex < 0 && !isAboutToEvolve)
        {
            if (!isAboutToEvolve)
            {
                // Trainer bought a Pokemon, but the bench is full, BUT the purchased
                // Pokemon can be used in an evolution to free up space
                return null;
            }
            var tuple = arenaService.GetContainerWithPokemonToEvolve(trainer, pokemon);
            containerType = tuple.Item1;
            benchIndex = tuple.Item2;
        }
        logger.LogInformation($"Trainer {trainer.Account.Username} {trainer.Id} bought a {pokemon.name} for {pokemon.tier}");
        shop.ShopPokemon[shopIndex] = null;
        trainer.Money -= cost;
        BuyPokemonDTO dto = await pokemonPoolService.Evolve(trainersService.TrainersPokemon[trainer.Id], pokemon);
        dto.TrainerId = trainer.Id;
        dto.Pokemon = pokemon;
        dto.Shop = new RefreshShopDTO()
        {
            Money = trainer.Money,
            Shop = shop
        };
        dto.Move = new MovePokemonDTO()
        {
            TrainerId = trainer.Id,
            PokemonId = pokemon.Id,
            PokeContainerIndex = benchIndex,
            ContainerType = containerType
        };
        return dto;
    }
}
