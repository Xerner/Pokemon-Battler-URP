using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoChess.Contracts.DTOs;
using AutoChess.Contracts.Interfaces;
using AutoChess.Contracts.Models;
using AutoChess.Contracts.Options;
using AutoChess.Contracts.Repositories;
using AutoChess.Infrastructure.Context;
using AutoChess.Library.Extensions;
using AutoChess.Library.Interfaces;

namespace AutoChess.Library.Services;

public class UnitService(ILogger<UnitService> logger,
                             AutoChessContext context,
                             IUnitQueryService autoChessUnitQueryService,
                             IUnitCountService unitCountService,
                             IPoolOptions poolOptions) : IUnitService
{
    public const int MAX_SELL_VALUE = 50;

    #region Public Methods

    /// <summary>
    /// Adds a Unit to the Players active units if there is room on the bench, or if a unit will combine with other units on the bench
    /// </summary>
    /// <param name="unit">The Unit to attempt to add to the bench</param>
    /// <param name="container">The container to add it to</param>
    public async Task<bool> CanClaimUnit(Game game, Player player, Unit unit, IAutoChessUnitContainer container)
    {
        var notEnoughMoney = player.Money < unit.Cost;
        if (notEnoughMoney)
        {
            return false;
        }
        if (IsUnitClaimed(unit))
        {
            return false;
        }
        if (container is null)
        {
            var playersUnits = await autoChessUnitQueryService.GetUnits(game, player);
            bool canCombine = playersUnits.CanCombine(unit);
            if (canCombine == false)
            {
                return false; // no fucking room
            }
        }
        return true;
    }

    public async Task<Unit[]> WithdrawManyAsync(Game game, Player player, IEnumerable<Unit> unitsToReturn, int count)
    {
        DepositMany(unitsToReturn);
        Unit[] units = new Unit[count];
        for (int i = 0; i < units.Length; i++)
        {
            units[i] = await Withdraw(game, player);
        }
        logger.LogInformation($"Units withdrawed: " + string.Join(',', units.Select(unit => unit.ToString())));
        return units;
    }

    public void DepositMany(IEnumerable<Unit> units)
    {
        foreach (var unit in units)
        {
            Deposit(unit);
        }
    }

    public void Deposit(Unit unit)
    {
        unit.AccountId = null;
    }

    public bool IsUnitClaimed(Unit unit)
    {
        return unit.AccountId is not null && unit.Container is not null;
    }

    #endregion

    #region Private Methods

    /// <summary>Attempt to withdraw 1 Pokemon from the PokemonPoolService</summary>
    /// <returns>A random Pokemon from the given tier, or null if there are no Pokemon left to pull</returns>
    async Task<Unit> Withdraw(Game game, Player player)
    {
        var tier = RollForTier(player.Level);
        var randomUnit = await RollForAUnitAsync(game, tier);
        randomUnit.AccountId = player.AccountId;
        return randomUnit;
    }

    async Task<Unit> RollForAUnitAsync(Game game, int tier, bool remove = false)
    {
        var random = new Random();
        var unitInfosOfTier = await context.Games.Where(game => game.Id == game.Id)
            .Include(game => game.UnitInfos)
            .Select(game => game.UnitInfos.Where(info => info.Tier == tier))
            .SelectMany(unitInfos => unitInfos)
            .ToListAsync();
        var rolledUnits = new HashSet<Unit>();
        while (rolledUnits.Count <= unitInfosOfTier.Count)
        {
            int roll = random.Next(0, unitInfosOfTier.Count + 1);
            var randomUnitInfo = unitInfosOfTier[roll];
            // TODO: this process should be changed to use a number count, because of how complicated selling
            //       combined units and generated units that shouldnt be counted in the pool will be (instead
            //       of relying on the count of units unclaimed in the pool). This also implies a new property
            //       on the AutoChessUnitInfo model needs to be added to determine if the unit should be counted
            //       in the pool or not, and how many counts one unit should be worth
            var countOfAvailableUnits = await unitCountService.GetUnitCount(game.Id, randomUnitInfo.Id);
            if (countOfAvailableUnits is null)
            {
                throw new Exception($"Unit count is null for unit info {randomUnitInfo}");
            }
            if (TryCreateUnit(randomUnitInfo, countOfAvailableUnits, true, out Unit? unit))
            {
                return unit!;
            }
        }
        logger.LogInformation($"Cannot withdraw a unit of tier {tier}! Somehow, there are 0 left");
        throw new Exception($"Cannot withdraw a unit of tier {tier}! Somehow, there are 0 left");
    }

    /// <summary>Roll for what tier of Pokemon the trainer will pull from the PokemonPoolService</summary>
    /// <returns>The tier of unit to pull from the Pokemon PokemonPoolService</returns>
    int RollForTier(int trainerLevel)
    {
        var random = new Random();
        float roll = random.Next(1, 101);
        int chanceSum = 0;
        // Example: 
        // rolls 90, trainer level 3
        // {75, 25, 0, 0, 0}
        // 90 < 75 for a tier 1? no
        // 25 + 75 = 95
        // 90 < 95 for a tier 2? yes
        // rolls a tier 2 unit
        for (int i = 0; i < 5; i++)
        {
            chanceSum += poolOptions.TierChancesByLevel[trainerLevel][i];
            if (roll < chanceSum)
            {
                return i + 1;
            }
        }
        throw new Exception("Failed all tier chances when rolling for a unit");
    }

    bool TryCreateUnit(UnitInfo unitInfo, UnitCount unitCount, bool isCountedInPool, out Unit? unit)
    {
        unit = default;
        if (CanCreateUnit(unitCount) == false)
        {
            return false;
        }
        unitCount.Count--;
        unit = CreateUnit(unitInfo, isCountedInPool);
        return true;
    }

    Unit CreateUnit(UnitInfo unitInfo, bool isCountedInPool)
    {
        var unit = new Unit(isCountedInPool, 1)
        {
            Info = unitInfo,
            InfoId = unitInfo.Id
        };
        unit.Cost = GetCost(unit);
        unit.SellValue = GetSellValue(unit);
        return unit;
    }

    bool CanCreateUnit(UnitCount unitCount) => unitCount.Count != 0;

    public int GetSellValue(Unit unit)
    {
        return Math.Clamp(GetCost(unit) * unit.CombinationStage - 1, 0, MAX_SELL_VALUE);
    }

    public int GetCost(Unit unit)
    {
        return unit.Info.Tier;
    }

    #endregion

    #region TODO: move to proper service

    /// <summary>
    /// Recursively evolve the pokemon until it can't no more
    /// </summary>
    public async Task<UnitClaimedDTO> Evolve(List<Pokemon> trainersPokemon, Pokemon pokemon, List<Guid> pokemonToDestroy = null)
    {
        if (!trainersPokemon.IsAboutToEvolve(pokemon))
        {
            return new UnitClaimedDTO()
            {
                Unit = pokemon,
                UnitsToDestroy = null
            };
        }
        // TODO: move this function to proper place
        if (pokemonToDestroy == null)
        {
            pokemonToDestroy = [];
        }
        bool evolving = trainersPokemon.IsAboutToEvolve(pokemon);
        if (!evolving)
        {
            return new UnitClaimedDTO()
            {
                Unit = pokemon,
                UnitsToDestroy = pokemonToDestroy
            };
        }
        // Destroy all other instances of this Pokemon
        var otherPokemonWithSameName = trainersPokemon.Where(p => p.Name == pokemon.Name);
        foreach (Pokemon otherPokemon in trainersPokemon)
        {
            if (pokemon != otherPokemon && otherPokemon.Name == pokemon.Name)
            {
                pokemonToDestroy.Add(otherPokemon.Id);
            }
        }
        foreach (Pokemon otherPokemon in trainersPokemon.Where(p => pokemonToDestroy.Contains(p.Id)))
        {
            trainersPokemon.Remove(otherPokemon);
        }
        // Evooooolve
        var id = pokemon.Id;
        pokemon = await pokeApiService.GetEvolution(pokemon);
        pokemon.Id = id;
        // recursion is intended here
        // If the evolved Pokemon is about to evolve, then it will be evolved again
        return await Evolve(trainersPokemon, pokemon, pokemonToDestroy);
    }

    #endregion
}
