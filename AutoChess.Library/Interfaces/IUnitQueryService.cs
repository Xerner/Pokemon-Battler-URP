using AutoChess.Contracts.Interfaces;
using AutoChess.Contracts.Models;
using AutoChess.Infrastructure.Context;

namespace AutoChess.Library.Interfaces;

public interface IUnitQueryService
{
    Unit? GetSimilarUnit(Unit unit, IEnumerable<Unit> units);
    Task<IEnumerable<Unit>> GetUnits(Game game, Player player, AutoChessContext context);
    IQueryable<Unit> GetUnitsQuery(Game game, Player player, AutoChessContext context);
}
