using AutoChess.Contracts.Models;

namespace AutoChess.Library.Interfaces;

public interface IGameService
{
    Task<Game> CreateGameAsync();
    Task<IEnumerable<Player>> GetPlayers(Game game);
}
