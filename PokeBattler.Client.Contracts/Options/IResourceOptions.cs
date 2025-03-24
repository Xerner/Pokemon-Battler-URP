using System.Collections.Generic;

namespace AutoChess.Contracts.Options
{
    public interface IResourceOptions
    {
        public const string Key = "ResourceOptions";

        /// <summary>
        /// Base income for a player
        /// </summary>
        public int BaseIncome { get; }
        /// <summary>
        /// Income bonus for winning a pvp match
        /// </summary>
        public int PvpWinIncome { get; }
        /// <summary>
        /// Win streak count to income bonus
        /// </summary>
        public List<int> WinStreakIncome { get; }
        public List<int> LevelToExpNeededToLevelUp { get; }
    }
}
