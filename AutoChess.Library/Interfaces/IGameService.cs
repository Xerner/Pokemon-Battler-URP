using AutoChess.Contracts.Models;
using AutoChess.Contracts.Options;

namespace AutoChess.Library.Interfaces;

public interface IGameService
{
    Task<Game> CreateGameAsync(IGameOptions gameOptions);
    Task<Game?> GetGameAsync(Guid gameId);
    Task<IEnumerable<Player>> GetPlayers(Game game);
}
