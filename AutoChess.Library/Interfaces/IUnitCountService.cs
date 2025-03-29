using AutoChess.Contracts.Models;
using AutoChess.Infrastructure.Context;

namespace AutoChess.Library.Interfaces;

public interface IUnitCountService
{
    Task<UnitCount?> GetUnitCount(Guid gameId, Guid unitInfoId, AutoChessContext context);
}

