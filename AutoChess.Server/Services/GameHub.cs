using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using AutoChess.Contracts.Models;
using AutoChess.Contracts.Interfaces;
using AutoChess.Contracts.DTOs;
using AutoChess.Contracts.Options;
using AutoChess.Library.Interfaces;
using AutoChess.Infrastructure.Context;
using AutoChess.Server.Extensions;

namespace AutoChess.Server.Services;

public class GameHub(ILogger<GameHub> logger,
                     AutoChessContext context,
                     IGameService gameService,
                     IShopService shopService,
                     IPlayerService playerService,
                     PoolOptions poolOptions,
                     GameOptions defaultGameOptions) : Hub<IHubClient>, IHubServer
{
    /// <summary>
    /// Ping! Pong!
    /// </summary>
    /// <param name="str">Ping!</param>
    /// <returns>Pong!</returns>
    public string Ping(string str)
    {
        return str;
    }

    public async Task<Game?> CreateGameAsync(Guid accountId)
    {
        var account = await context.Accounts.FirstOrDefaultAsync(account => account.Id == accountId);
        if (account == null) {
            logger.LogInformation($"Account {accountId} not found");
            return null;
        }
        var game = gameService.CreateGame(defaultGameOptions, context);
        var player = await playerService.CreateOrFetchExisting(account, game, context);
        await context.SaveChangesAsync();
        return game;
    }

    public async Task<bool> AddToGame(Guid accountId, Guid gameId)
    {
        var account = await context.Accounts.FirstOrDefaultAsync(account => account.Id == accountId);
        var game = await context.Games.FirstOrDefaultAsync(game => game.Id == gameId);
        if (account == null || game == null)
        {
            logger.LogInformation($"Account {accountId} or game {gameId} not found");
            return false;
        }
        var player = await playerService.CreateOrFetchExisting(account, game, context);
        await context.SaveChangesAsync();
        await Clients.BroadcastToGame(gameId).AddPlayerToGame(player);
        return true;
    }

    public async Task<bool> UpdateTrainerReady(Guid accountId, Guid gameId, bool isReady)
    {
        await playerService.UpdateTrainerReady(accountId, gameId, isReady, context);
        await context.SaveChangesAsync();
        _ = Clients.BroadcastToGame(gameId).UpdateTrainerReady(accountId, gameId, isReady);
        return true;
    }

    public IEnumerable<int> GetTierChances(int playerLevel)
    {
        return poolOptions.TierChancesByLevel[playerLevel];
    }

    public async Task<RefreshShopDTO?> RefreshShop(Guid accountId, Guid gameId)
    {
        var player = await playerService.GetPlayerAsync(accountId, gameId, context);
        var game = await gameService.GetGameAsync(gameId, context);
        if (player is null || game is null)
        {
            logger.LogInformation($"Player with accountId {accountId} or gameId {gameId} not found");
            return null;
        }
        var dto = await shopService.RefreshShopAsync(game, player, false, context);
        await context.SaveChangesAsync();
        return dto;
    }

    public async Task<BuyExperienceDTO?> BuyExperience(Guid accountId, Guid gameId)
    {
        var player = await context.Players.FirstOrDefaultAsync(player => player.AccountId == accountId && player.GameId == gameId);
        if (player is null)
        {
            logger.LogInformation($"Player with accountId {accountId} and gameId {gameId} not found");
            return null;
        }
        var previousLevel = player.Level;
        var dto = shopService.BuyExperience(player);
        await context.SaveChangesAsync();
        if (player.Level > previousLevel)
        {
            var playerLevelUpDTO = new PlayerLevelUpDTO()
            {
                Id = accountId,
                Level = player.Level,
            };
            _ = Clients.BroadcastToGame(gameId).PlayerLevelUp(playerLevelUpDTO);
        }
        return dto;
    }

    public async Task<bool> TryToBuyUnit(Guid accountId, Guid gameId, Guid unitId)
    {
        var game = await gameService.GetGameAsync(gameId, context);
        var player = await playerService.GetPlayerAsync(accountId, gameId, context);
        var unit = await context.Units.FirstOrDefaultAsync(unit => unit.Id == unitId);
        if (game is null || player is null || unit is null)
        {
            logger.LogInformation($"Game with gameId {gameId}, player with accountId {accountId} or unit with unitId {unitId} not found");
            return false;
        }
        var dto = await shopService.TryToBuyUnit(game, player, unit, context);
        await context.SaveChangesAsync();
        await Clients.All.UnitClaimed(dto); 
        return true;
    }
}
