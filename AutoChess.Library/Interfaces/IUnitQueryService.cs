using AutoChess.Contracts.Interfaces;
using AutoChess.Contracts.Models;

namespace AutoChess.Library.Interfaces;

public interface IUnitQueryService
{
    Unit? GetSimilarUnit(Unit unit, IEnumerable<Unit> units);
    Task<IEnumerable<Unit>> GetUnits(Game game, Player player);
    IQueryable<Unit> GetUnitsQuery(Game game, Player player);
}
