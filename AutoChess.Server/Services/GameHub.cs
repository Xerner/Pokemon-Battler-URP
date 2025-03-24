using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using AutoChess.Contracts.Models;
using AutoChess.Contracts.Interfaces;
using AutoChess.Contracts.Repositories;
using AutoChess.Library.Interfaces;
using AutoChess.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using AutoChess.Contracts.DTOs;
using AutoChess.Server.Extensions;
using System.Collections.Generic;

namespace AutoChess.Server.Services;

public class GameHub(IGameService gameService,
                     IUnitService pokemonPoolService,
                     IShopService shopService,
                     IPlayerService playerService,
                     AutoChessContext context
                     ) : Hub<IHubClient>, IHubServer
{
    public const string HubName = "Games";

    public string Ping(string str)
    {
        return str;
    }

    #region GameService

    public async Task<Game> CreateGameAsync()
    {
        var game = await gameService.CreateGameAsync();
        return game;
    }

    #endregion

    #region

    public async Task AddToGame(Guid accountId, Guid gameId)
    {
        var account = await context.Accounts.FirstOrDefaultAsync(account => account.Id == accountId);
        var game = await context.Games.FirstOrDefaultAsync(game => game.Id == gameId);
        var player = await playerService.CreateOrReconnect(account, game);
        await Clients.Group(gameId.ToString()).AddPlayerToGame(player);
    }

    #endregion

    public async Task<bool> UpdateTrainerReady(Guid accountId, Guid gameId, bool isReady)
    {
        await playerService.UpdateTrainerReady(accountId, gameId, isReady);
        _ = Clients.BroadcastToGame(gameId).UpdateTrainerReady(accountId, gameId, isReady);
        return true;
    }

    #region ShopService

    public IEnumerable<int> GetTierChances(int trainerLevel)
    {
        var tierChances = pokemonPoolService.GetTierChances(trainerLevel);
        return tierChances;
    }

    public async Task<RefreshShopDTO> RefreshShop(Guid accountId, Guid gameId)
    {
        var player = await playerService.GetPlayerAsync(accountId, gameId);
        var dto = shopService.RefreshShop(player);
        return dto;
    }

    public BuyExperienceDTO BuyExperience(Guid id)
    {
        var trainer = playerService.GetPlayer(id);
        var level = trainer.Level;
        var dto = shopService.BuyExperience(trainer);~
        // TODO: implement this in the client
        if (trainer.Level == level)
        {
            var trainerLevelUpDTO = new PlayerLevelUpDTO()
            {
                Id = id,
                Level = level,
            };
            Clients.All.PlayerLevelUp(trainerLevelUpDTO);
        }
        return dto;
    }

    public async Task ClaimUnit(Guid accountId, Guid gameId, Guid unitId)
    {
        var player = playerService.GetPlayerAsync(accountId, gameId);
        var dto = await shopService.BuyPokemon(player, shopIndex, pokemonId);
        await Clients.All.UnitClaimed(dto);
    }

    #endregion
}
