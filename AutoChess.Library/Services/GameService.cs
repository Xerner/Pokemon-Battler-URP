using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoChess.Contracts.Models;
using AutoChess.Contracts.Options;
using AutoChess.Infrastructure.Context;
using AutoChess.Library.Interfaces;

namespace AutoChess.Library.Services;

public class GameService(ILogger<GameService> logger,
                         AutoChessContext context) : IGameService
{
    public async Task<Game?> GetGameAsync(Guid gameId)
    {
        return await context.Games.Where(game => game.Id == gameId).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Player>> GetPlayers(Game game)
    {
        return await context.Players.Where(player => player.GameId == game.Id).ToListAsync();
    }

    public async Task<Game> CreateGameAsync(IGameOptions gameOptions)
    {
        // This should be done differently when I have time. It should load pokemon on
        // game launch based on a ScriptableObject or something, and then cache them somehow
        // for reuse on next game launch
        var game = new Game()
        {
            Id = Guid.NewGuid(),
            GameOptions = gameOptions,
        };
        context.Games.Add(game);
        logger.LogInformation($"Game {game.Id} created");
        return game;
    }
}
