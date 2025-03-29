using Microsoft.Extensions.Logging;
using AutoChess.Contracts.DTOs;
using AutoChess.Contracts.Models;
using AutoChess.Library.Interfaces;
using AutoChess.Contracts.Repositories;
using AutoChess.Contracts.Options;
using AutoChess.Contracts.Enums;
using Microsoft.EntityFrameworkCore;
using AutoChess.Library.Extensions;
using AutoChess.Infrastructure.Context;
using Microsoft.Extensions.Options;

namespace AutoChess.Library.Services;

public class ShopService(ILogger<ShopService> logger, 
                         IPlayerService playersService, 
                         IUnitService unitService,
                         IUnitQueryService unitQueryService,
                         IUnitContainerService unitContainerService,
                         IOptions<ResourceOptions> resourceOptions,
                         IOptions<GameOptions> gameOptions,
                         IOptions<PoolOptions> poolOptions) : IShopService
{
    public BuyExperienceDTO BuyExperience(Player player)
    {
        if (!gameOptions.Value.FreeExperience)
        {
            if (player.Money < player.ExperienceCost)
            {
                logger.LogInformation("Not enough money to buy experience!");
                return new BuyExperienceDTO()
                {
                    Money = player.Money,
                    Level = player.Level,
                    Experience = player.Experience,
                    ExperienceNeededToLevelUp = resourceOptions.Value.LevelToExpNeededToLevelUp[player.Level],
                    TierChances = poolOptions.Value.TierChancesByLevel[player.Level]
                };
            }
            player.Money -= player.ExperienceCost;
        }
        playersService.AddExperience(player, player.ExperienceCost);
        player.ExperienceNeededToLevelUp = resourceOptions.Value.LevelToExpNeededToLevelUp[player.Level];
        return new BuyExperienceDTO()
        {
            Money = player.Money,
            Level = player.Level,
            Experience = player.Experience,
            ExperienceNeededToLevelUp = player.ExperienceNeededToLevelUp,
            TierChances = poolOptions.Value.TierChancesByLevel[player.Level]
        };
    }

    public async Task<RefreshShopDTO> RefreshShopAsync(Game game, Player player, bool freeShop, AutoChessContext context)
    {
        var isShopFree = freeShop || gameOptions.Value.FreeRefreshShop;
        if (isShopFree == false)
        {
            if (player.Money < player.ShopCost)
            {
                logger.LogInformation("Not enough money to refresh the shop!");
                return new RefreshShopDTO() {
                    NewMoneyBalance = player.Money,
                    NewUnits = []
                };
            }
            player.Money -= player.ShopCost;
        }
        var unitsToReturn = await GetUnitsInShop(game, player, context);
        var newUnits = await unitService.WithdrawManyAsync(game, player, unitsToReturn, gameOptions.Value.ShopSize, context);
        return new RefreshShopDTO()
        {
            NewMoneyBalance = player.Money,
            NewUnits = newUnits
        };
    }

    public async Task<UnitClaimedDTO?> TryToBuyUnit(Game game, Player player, Unit unit, AutoChessContext context)
    {
        var availableBenchContainer = (await unitContainerService.GetContainersWithTags(player, EContainerTag.Bench, context)).FirstOrDefault();
        var playersUnits = await unitQueryService.GetUnits(game, player, context);
        var canClaimUnit = unitService.CanClaimUnit(game, player, unit, playersUnits, availableBenchContainer);
        if (canClaimUnit == false)
        {
            return null;
        }
        unitService.ClaimUnit(player, unit);
        player.Money -= unitService.GetCost(unit);
        var canBeCombined = playersUnits.CanBeCombined(unit);
        var unitClaimedDto = new UnitClaimedDTO()
        {
            AccountId = player.AccountId,
            NewMoneyBalance = player.Money,
            UnitId = unit.Id
        };
        if (canBeCombined)
        {
            var existingSimilarUnit = unitQueryService.GetSimilarUnit(unit, playersUnits);
            unitClaimedDto.UnitsToDestroy = unitService.PromoteUnit(existingSimilarUnit!, playersUnits).Select(unit => unit.Id);
            unitClaimedDto.UnitPromoted = existingSimilarUnit!.Id;
            return unitClaimedDto;
        }
        if (availableBenchContainer is null)
        {
            throw new Exception("Unit was purchased with no available bench container found and unit cannot be combined!");
        }
        unitClaimedDto.Move = new MoveUnitDTO()
        {
            AccountId = player.AccountId,
            UnitId = unit.Id,
            ContainerId = availableBenchContainer.Id,
        };
        return unitClaimedDto;
    }

    public async Task<IEnumerable<Unit>> GetUnitsInShop(Game game, Player player, AutoChessContext context)
    {
        var units = await unitQueryService.GetUnitsQuery(game, player, context)
            .Where(unit => unit.Container == null)
            .ToListAsync();
        return units;

    }
}
