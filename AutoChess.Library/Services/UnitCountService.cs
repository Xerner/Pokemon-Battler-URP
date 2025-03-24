using AutoChess.Contracts.Models;
using AutoChess.Infrastructure.Context;
using AutoChess.Library.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoChess.Library.Services;

public class UnitCountService(AutoChessContext context) : IUnitCountService
{
    public async Task<UnitCount?> GetUnitCount(Guid gameId, Guid unitInfoId)
    {
        var unitCount = await context.UnitCounts
            .Where(uc => uc.GameId == gameId && uc.UnitInfoId == unitInfoId)
            .FirstOrDefaultAsync();
        return unitCount;
    }
}
