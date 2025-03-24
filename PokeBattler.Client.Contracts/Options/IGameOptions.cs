using AutoChess.Contracts.Enums;
using System.Diagnostics.Contracts;

namespace AutoChess.Contracts.Options
{
    public interface IGameOptions
    {
        public const string Key = "GameOptions";

        public bool FreeRefreshShop { get; }
        public bool FreeExperience { get; }
        public bool FreePokemon { get; }
        public int ShopSize { get; }
        public float RoundTime { get; }
        public float OvertimeRoundTime { get; }
        public int PlayerCount { get; }
        public LogLevel DebugLogLevel { get; }
    }
}
