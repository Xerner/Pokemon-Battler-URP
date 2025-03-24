using AutoChess.Contracts.Enums;
using AutoChess.Contracts.Interfaces;
using AutoChess.Contracts.Models;

namespace AutoChess.Library.Interfaces;

public interface IUnitContainerService
{
    Task<IEnumerable<IAutoChessUnitContainer>> GetContainersWithTags(Player player, EContainerType tags);
}
