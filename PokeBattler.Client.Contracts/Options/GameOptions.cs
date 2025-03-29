using AutoChess.Contracts.Enums;

namespace AutoChess.Contracts.Options
{
    public class GameOptions
    {
        public const string Key = "GameOptions";

        public bool FreeRefreshShop { get; set; }
        public bool FreeExperience { get; set; }
        public bool FreePokemon { get; set; }
        public int ShopSize { get; set; }
        public float RoundTime { get; set; }
        public float OvertimeRoundTime { get; set; }
        public int PlayerCount { get; set; }
        public LogLevel DebugLogLevel { get; set; }
    }
}
