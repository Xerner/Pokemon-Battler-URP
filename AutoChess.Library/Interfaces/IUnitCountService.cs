using AutoChess.Contracts.Models;

namespace AutoChess.Library.Interfaces;

public interface IUnitCountService
{
    Task<UnitCount?> GetUnitCount(Guid gameId, Guid unitInfoId);
}

