using AutoChess.Contracts.Models;

namespace AutoChess.Library.Interfaces;

public interface IUnitInfoService
{
    Task AddToGameAsync(Game game, IEnumerable<UnitInfo> units);
}
