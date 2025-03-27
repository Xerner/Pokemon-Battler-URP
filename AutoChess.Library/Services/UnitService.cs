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
    public bool CanClaimUnit(Game game, Player player, Unit unit, IEnumerable<Unit> playersOtherUnits, IUnitContainer? container)
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
            bool canCombine = playersOtherUnits.CanBeCombined(unit);
            if (canCombine == false)
            {
                return false; // no fucking room
            }
        }
        return true;
    }

    public void ClaimUnit(Player player, Unit unit)
    {
        unit.AccountId = player.AccountId;
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

    /// <returns>The Units to destroy</returns>
    public IEnumerable<Unit> PromoteUnit(Unit unit, IEnumerable<Unit> playersUnits)
    {
        if (unit.CombinationStage == 3)
        {
            return [];
        }
        var otherUnitsOfSameStage = playersUnits.Where(u => u.InfoId == unit.InfoId && u.CombinationStage == unit.CombinationStage && u.Id != unit.Id);
        if (otherUnitsOfSameStage.Count() < 2)
        {
            return [];
        }
        unit.CombinationStage++;
        return otherUnitsOfSameStage;
    }

    #endregion

    #region Private Methods

    async Task<Unit> Withdraw(Game game, Player player)
    {
        var tier = RollForTier(player.Level);
        var randomUnit = await CreateRandomUnit(game, tier);
        randomUnit.AccountId = player.AccountId;
        return randomUnit;
    }

    async Task<Unit> CreateRandomUnit(Game game, int tier, bool remove = false)
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
}
