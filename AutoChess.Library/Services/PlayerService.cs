using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoChess.Contracts.Interfaces;
using AutoChess.Contracts.Models;
using AutoChess.Contracts.Options;
using AutoChess.Infrastructure.Context;
using AutoChess.Library.Interfaces;

namespace AutoChess.Library.Services;

public class PlayerService(ILogger<PlayerService> logger,
                             IGameService gameService,
                             IResourceOptions resourceOptions,
                             AutoChessContext context,
                             IGameOptions gameOptions) : IPlayerService
{
    public async Task<bool> UpdateTrainerReady(Guid accountId, Guid gameId, bool ready)
    {
        var trainer = await context.Players.FirstOrDefaultAsync(player => player.AccountId == accountId && player.GameId == gameId);
        if (trainer is null)
        {
            logger.LogInformation($"Trainer with accountId {accountId} and gameId {gameId} not found");
            return false;
        }
        trainer.Ready = ready;
        await context.SaveChangesAsync();
        return trainer.Ready;
    }

    public async Task<Player?> GetPlayerAsync(Guid accountId, Guid gameId)
    {
        return await context.Players.FirstOrDefaultAsync(player => player.GameId == gameId && player.AccountId == accountId);
    }

    public async Task<Player> CreateOrReconnect(Account account, Game game)
    {
        var players = await gameService.GetPlayers(game);
        var player = players.FirstOrDefault(p => p.AccountId == account.Id);
        //var player = new Player(account, resourceOptions.LevelToExpNeededToLevelUp[1]);
        // if trainer was already in-game and is reconnecting
        if (player is not null)
        {
            logger.LogInformation($"Reconnecting player with username {account.Username} to game {game.Id}");
            throw new NotImplementedException();
        }
        // gameService slots are full
        if (players.Count() >= gameOptions.PlayerCount)
        {
            logger.LogInformation($"Game is full, rejecting player with username {account.Username}");
            throw new NotImplementedException();
        }
        logger.LogInformation($"Created player with username {account.Username} in game {game.Id}");
        player = new Player()
        {
            AccountId = account.Id,
            GameId = game.Id
        };
        context.Players.Add(player);
        await context.SaveChangesAsync();
        return player;
    }

    public int CalculateInterest(Player trainer) => trainer.Money / 10;

    /// <summary>Create experience to the Trainer. Rolls over exp if they level up.</summary>
    /// <returns>True if the player levels up</returns>
    public async Task<bool> AddExperience(Player player, int expToAdd)
    {
        int expNeeded = resourceOptions.LevelToExpNeededToLevelUp[player.Level];
        int newExp = player.Experience + expToAdd;
        int difference = expNeeded - newExp;
        if (difference <= 0)
        { // leveled up
          // edge case: player is already max level
            var isPlayerMaxLevel = resourceOptions.LevelToExpNeededToLevelUp.Count() - 1 >= player.Level;
            if (isPlayerMaxLevel == false)
            {
                player.Experience = resourceOptions.LevelToExpNeededToLevelUp[player.Level];
                difference = 1;
            }
            else
            {
                player.Level++;
                player.Experience = Math.Abs(difference);
            }
        }
        else
        {
            player.Experience = newExp;
        }
        var leveledUp = difference <= 0;
        await context.SaveChangesAsync();
        return leveledUp;
    }
}
