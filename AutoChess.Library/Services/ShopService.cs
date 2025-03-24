using Microsoft.Extensions.Logging;
using AutoChess.Contracts.DTOs;
using AutoChess.Contracts.Models;
using AutoChess.Library.Interfaces;
using AutoChess.Contracts.Repositories;
using AutoChess.Contracts.Options;
using AutoChess.Contracts.Enums;
using Microsoft.EntityFrameworkCore;

namespace AutoChess.Library.Services;

public class ShopService(ILogger<ShopService> logger, 
                         IPlayerService playersService, 
                         IResourceOptions resourceOptions,
                         IArenaService arenaService,
                         IUnitService unitService,
                         IUnitQueryService autoChessUnitService,
                         IGameOptions gameOptions,
                         IPoolOptions poolOptions,
                         IGameService gameService) : IShopService
{
    public const int ExperienceCost = 4;
    public const int ShopCost = 2;

    //readonly Dictionary<Guid, TrainerShop> TrainerShops = [];

    public BuyExperienceDTO BuyExperience(Player player)
    {
        if (!gameOptions.FreeExperience)
        {
            if (player.Money < ExperienceCost)
            {
                logger.LogInformation("Not enough money to buy experience!");
                return new BuyExperienceDTO()
                {
                    Money = player.Money,
                    Level = player.Level,
                    Experience = player.Experience,
                    ExperienceNeededToLevelUp = resourceOptions.LevelToExpNeededToLevelUp[player.Level],
                    TierChances = poolOptions.TierChancesByLevel[player.Level]
                };
            }
            player.Money -= ExperienceCost;
        }
        playersService.AddExperience(player, ExperienceCost);
        player.ExperienceNeededToLevelUp = resourceOptions.LevelToExpNeededToLevelUp[player.Level];
        return new BuyExperienceDTO()
        {
            Money = player.Money,
            Level = player.Level,
            Experience = player.Experience,
            ExperienceNeededToLevelUp = player.ExperienceNeededToLevelUp,
            TierChances = poolOptions.TierChancesByLevel[player.Level]
        };
    }

    public async Task<RefreshShopDTO> RefreshShop(Game game, Player player, bool freeShop)
    {
        var isShopFree = freeShop || gameOptions.FreeRefreshShop;
        if (isShopFree == false)
        {
            if (player.Money < ShopCost)
            {
                logger.LogInformation("Not enough money to refresh the shop!");
                return new RefreshShopDTO() {
                    NewMoneyBalance = player.Money,
                    ShopUnits = []
                };
            }
            player.Money -= ShopCost;
        }
        var unitsToReturn = await GetUnitsInShop(game, player);
        var newUnits = await unitService.WithdrawManyAsync(game, player, unitsToReturn, gameOptions.ShopSize);
        return new RefreshShopDTO()
        {
            NewMoneyBalance = player.Money,
            ShopUnits = newUnits
        };
    }

    public async Task<BuyPokemonDTO> BuyPokemon(Player player, Unit unit)
    {
        var shop = TrainerShops[player.Id];
        var pokemon = await pokemonApiService.GetPokemon(pokemonId);
        var cost = unitService.GetCost(pokemon);
        if (player.Money < cost && !IShopService.FreeUnits)
        {
            logger.LogInformation("Not enough money");
            return null;
        }
        pokemon.Id = new Guid();
        var containerType = EContainerType.Bench;
        var benchIndex = arenaService.GetAvailableBenchIndex(player);
        var isAboutToEvolve = playersService.TrainersPokemon[player.Id].IsAboutToEvolve(pokemon);
        if (benchIndex < 0 && !isAboutToEvolve)
        {
            if (!isAboutToEvolve)
            {
                // Trainer bought a Pokemon, but the bench is full, BUT the purchased
                // Pokemon can be used in an evolution to free up space
                return null;
            }
            var tuple = arenaService.GetContainerWithPokemonToEvolve(player, pokemon);
            containerType = tuple.Item1;
            benchIndex = tuple.Item2;
        }
        logger.LogInformation($"Trainer {player.Account.Username} {player.Id} bought a {pokemon.name} for {pokemon.tier}");
        shop.ShopPokemon[shopIndex] = null;
        player.Money -= cost;
        BuyPokemonDTO dto = await unitService.Evolve(playersService.TrainersPokemon[player.Id], pokemon);
        dto.TrainerId = player.Id;
        dto.Pokemon = pokemon;
        dto.Shop = new RefreshShopDTO()
        {
            Money = player.Money,
            Shop = shop
        };
        dto.Move = new MovePokemonDTO()
        {
            TrainerId = player.Id,
            PokemonId = pokemon.Id,
            PokeContainerIndex = benchIndex,
            ContainerType = containerType
        };
        return dto;
    }

    public async Task<IEnumerable<Unit>> GetUnitsInShop(Game game, Player player)
    {
        var units = await autoChessUnitService.GetUnitsQuery(game, player)
            .Where(unit => unit.Container == null)
            .ToListAsync();
        return units;

    }
}
