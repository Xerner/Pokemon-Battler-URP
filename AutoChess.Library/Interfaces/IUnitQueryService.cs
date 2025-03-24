using AutoChess.Contracts.Interfaces;
using AutoChess.Contracts.Models;

namespace AutoChess.Library.Interfaces;

public interface IUnitQueryService
{
    Task<IEnumerable<Unit>> GetUnits(Game game, Player player);
    IQueryable<Unit> GetUnitsQuery(Game game, Player player);
}
