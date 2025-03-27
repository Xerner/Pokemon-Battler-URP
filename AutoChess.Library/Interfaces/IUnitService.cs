using AutoChess.Contracts.Interfaces;
using AutoChess.Contracts.Models;

namespace AutoChess.Contracts.Repositories;

public interface IUnitService
{
    Task<Unit[]> WithdrawManyAsync(Game game, Player player, IEnumerable<Unit> unitsToReturn, int count);
    void DepositMany(IEnumerable<Unit> units);
    void Deposit(Unit units);
    bool IsUnitClaimed(Unit unit);
    bool CanClaimUnit(Game game, Player player, Unit unit, IEnumerable<Unit> playersOtherUnits, IUnitContainer? container);
    int GetSellValue(Unit unit);
    int GetCost(Unit unit);
    IEnumerable<Unit> PromoteUnit(Unit unit, IEnumerable<Unit> playersUnits);
    void ClaimUnit(Player player, Unit unit);
}
