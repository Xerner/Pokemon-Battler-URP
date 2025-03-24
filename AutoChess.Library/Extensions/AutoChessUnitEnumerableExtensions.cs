using AutoChess.Contracts.Models;

namespace AutoChess.Library.Extensions;

public static class AutoChessUnitEnumerableExtensions
{
    public static bool CanCombine(this IEnumerable<Unit> playersUnits, Unit unit, int highestCombination = 3)
    {
        var isAlreadyLastCombination = unit.CombinationStage >= highestCombination;
        if (isAlreadyLastCombination)
        {
            return false;
        }
        var otherCopiesOfTheUnit = playersUnits.Where(unit_ => unit_.Info.Id == unit.Info.Id);
        var hasOtherUnits = otherCopiesOfTheUnit.Count() > 0;
        var hasEnoughOtherUnits = otherCopiesOfTheUnit.Count() > 1;
        return hasOtherUnits && hasEnoughOtherUnits;
    }
}
