using AutoChess.Contracts.Models;
using AutoChess.Contracts.Options;
using AutoChess.Infrastructure.Context;
using AutoChess.Library.Interfaces;

namespace AutoChess.Library.Services;

public class UnitInfoService(IPoolOptions poolOptions,
                                      AutoChessContext context) : IUnitInfoService
{
    public async Task AddToGameAsync(Game game, IEnumerable<UnitInfo> units)
    {
        foreach (var unit in units)
        {
            AddToGameAsync(game, unit);
        }
        await context.SaveChangesAsync();
    }

    void AddToGameAsync(Game game, UnitInfo unit)
    {
        // TODO
        throw new NotImplementedException();
        var count = poolOptions.TierCounts[unit.Tier];
        game.UnitInfos.Add(unit);
    }
}
