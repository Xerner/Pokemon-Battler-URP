using Microsoft.EntityFrameworkCore;
using AutoChess.Contracts.Enums;
using AutoChess.Contracts.Interfaces;
using AutoChess.Contracts.Models;
using AutoChess.Contracts.Extensions;
using AutoChess.Infrastructure.Context;
using AutoChess.Library.Interfaces;

namespace AutoChess.Library.Services;

public class UnitContainerService(AutoChessContext context) : IUnitContainerService
{
    public async Task<IEnumerable<IUnitContainer>> GetContainersWithTags(Player player, EContainerTag tags)
    {
        var containers = await context.UnitContainers.Where(container => container.GameId == player.GameId && container.AccountId == player.AccountId)
                                                     .Where(container => container.Tags.Is(tags))
                                                     .ToListAsync();
        return containers;
    }
}
