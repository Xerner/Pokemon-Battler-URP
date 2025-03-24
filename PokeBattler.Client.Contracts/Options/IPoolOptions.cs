using System.Collections.Generic;

namespace AutoChess.Contracts.Options
{
    public interface IPoolOptions
    {
        public const string Key = "PoolOptions";

        /// <summary>How many Pokemon are allowed in each respective tier</summary>
        public List<int> TierCounts { get; }
        /// <summary>Chance to roll a 1 tier, 2 tier, 3 tier, 4 tier, 5 tier respective to Trainer level</summary>
        public List<List<int>> TierChancesByLevel { get; }
    }
}
