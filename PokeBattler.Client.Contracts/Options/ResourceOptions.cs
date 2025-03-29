namespace AutoChess.Contracts.Options
{
    public class ResourceOptions
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
        public int[] WinStreakIncome { get; }
        public int[] LevelToExpNeededToLevelUp { get; }
    }
}
