namespace AutoChess.Contracts.Options
{
    public class PoolOptions
    {
        public const string Key = "PoolOptions";

        /// <summary>How many Units are allowed in each respective tier</summary>
        public int[] TierCounts { get; set; } = [];
        /// <summary>Chance to roll a 1 tier, 2 tier, 3 tier, 4 tier, 5 tier respective to Player level</summary>
        public int[][] TierChancesByLevel { get; set; } = [];
    }
}
