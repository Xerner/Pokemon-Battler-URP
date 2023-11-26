namespace PokeBattler.Models
{
    public class GameSettings
    {
        public bool FreeRefreshShop = false;
        public bool FreeExperience = false;
        public bool FreePokemon = false;
        public float RoundTime = 30f;
        public float OvertimeRoundTime = 10f;
        public LogLevel DebugLogLevel = LogLevel.All;
    }
}
