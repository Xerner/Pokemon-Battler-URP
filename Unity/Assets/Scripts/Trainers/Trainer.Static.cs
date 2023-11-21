using System.Collections.Generic;

namespace Poke.Core
{
    public partial class Trainer
    {
        public static readonly int baseIncome = 5;
        public static readonly int pvpWinIncome = 1;
        public static Dictionary<int, int> winStreakIncome = new Dictionary<int, int>() {
            { 1, 0 },
            { 2, 1 },
            { 3, 1 },
            { 4, 2 },
            { 5, 3 }
        };
        public static Dictionary<int, int> LevelToExpNeededToLevelUp = new Dictionary<int, int>() {
            { 1, 0 },
            { 2, 2 },
            { 3, 6 },
            { 4, 10 },
            { 5, 20 },
            { 6, 36 },
            { 7, 56 },
            { 8, 80 },
            { 9, 100 }
        };
    }
}
