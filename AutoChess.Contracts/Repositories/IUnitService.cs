using AutoChess.Contracts.Interfaces;
using AutoChess.Contracts.Models;

namespace AutoChess.Contracts.Repositories;

public interface IUnitService
{
    Task<Unit[]> WithdrawManyAsync(Game game, Player player, IEnumerable<Unit> unitsToReturn, int count);
    void DepositMany(IEnumerable<Unit> units);
    void Deposit(Unit units);
    bool IsUnitClaimed(Unit unit);
    Task<bool> CanClaimUnit(Game game, Player player, Unit unit, IAutoChessUnitContainer container);
    int GetSellValue(Unit unit);
    int GetCost(Unit unit);
}
