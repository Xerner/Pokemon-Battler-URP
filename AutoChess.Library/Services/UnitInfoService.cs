using AutoChess.Contracts.Models;
using AutoChess.Contracts.Options;
using AutoChess.Infrastructure.Context;
using AutoChess.Library.Interfaces;

namespace AutoChess.Library.Services;

public class UnitInfoService(PoolOptions poolOptions) : IUnitInfoService
{
    public async Task AddToGameAsync(Game game, IEnumerable<UnitInfo> units, AutoChessContext context)
    {
        foreach (var unit in units)
        {
            AddToGameAsync(game, unit, context);
        }
        await context.SaveChangesAsync();
    }

    void AddToGameAsync(Game game, UnitInfo unit, AutoChessContext context)
    {
        // TODO
        throw new NotImplementedException();
        var count = poolOptions.TierCounts[unit.Tier];
        game.UnitInfos.Add(unit);
    }
}
