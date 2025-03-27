using AutoChess.Contracts.Models;

namespace AutoChess.Library.Extensions;

public static class UnitEnumerableExtensions
{
    /// <summary>
    /// Whether the provided unit can combine with other units in the collection.
    /// </summary>
    public static bool CanBeCombined(this IEnumerable<Unit> playersUnits, Unit unit, int highestCombination = 3)
    {
        var isAlreadyLastCombination = unit.CombinationStage >= highestCombination;
        if (isAlreadyLastCombination)
        {
            return false;
        }
        var otherCopiesOfTheUnit = playersUnits.Where(unit_ => unit_.InfoId == unit.InfoId && unit_.Id != unit.Id);
        var hasOtherUnits = otherCopiesOfTheUnit.Count() > 0;
        var hasEnoughOtherUnits = otherCopiesOfTheUnit.Count() > 1;
        return hasOtherUnits && hasEnoughOtherUnits;
    }
}
