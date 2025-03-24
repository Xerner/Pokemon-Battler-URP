using Microsoft.EntityFrameworkCore;
using AutoChess.Contracts.Models;
using AutoChess.Infrastructure.Context;
using AutoChess.Library.Interfaces;

namespace AutoChess.Library.Services;

public class UnitQueryService(AutoChessContext context) : IUnitQueryService
{
    public IQueryable<Unit> GetUnitsQuery(Game game, Player player)
    {
        return context.Units.Where(u => u.AccountId == player.AccountId && player.GameId == game.Id);
    }

    public async Task<IEnumerable<Unit>> GetUnits(Game game, Player player)
    {
        return await GetUnitsQuery(game, player).ToListAsync();
    }
}
