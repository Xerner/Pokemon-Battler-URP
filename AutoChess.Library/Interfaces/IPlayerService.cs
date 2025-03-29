using AutoChess.Contracts.Models;
using AutoChess.Infrastructure.Context;

namespace AutoChess.Library.Interfaces;

public interface IPlayerService
{
    bool AddExperience(Player player, int expToAdd);
    int CalculateInterest(Player trainer);
    Task<Player> CreateOrFetchExisting(Account account, Game game, AutoChessContext context);
    Task<Player?> GetPlayerAsync(Guid accountId, Guid gameId, AutoChessContext context);
    Task<bool> UpdateTrainerReady(Guid accountId, Guid gameId, bool ready, AutoChessContext context);
}
