using AutoChess.Contracts.Enums;
using AutoChess.Contracts.Interfaces;
using AutoChess.Contracts.Models;
using AutoChess.Infrastructure.Context;

namespace AutoChess.Library.Interfaces;

public interface IUnitContainerService
{
    Task<IEnumerable<IUnitContainer>> GetContainersWithTags(Player player, EContainerTag tags, AutoChessContext context);
}
