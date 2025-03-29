using AutoChess.Contracts.Models;
using AutoChess.Infrastructure.Context;

namespace AutoChess.Library.Interfaces;

public interface IUnitInfoService
{
    Task AddToGameAsync(Game game, IEnumerable<UnitInfo> units, AutoChessContext context);
}
