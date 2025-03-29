using AutoChess.Contracts.Models;
using AutoChess.Contracts.Options;
using AutoChess.Infrastructure.Context;

namespace AutoChess.Library.Interfaces;

public interface IGameService
{
    Game CreateGame(GameOptions gameOptions, AutoChessContext context);
    Task<Game?> GetGameAsync(Guid gameId, AutoChessContext context);
    Task<IEnumerable<Player>> GetPlayers(Game game, AutoChessContext context);
}
