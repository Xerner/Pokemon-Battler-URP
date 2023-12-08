namespace PokeBattler.Common.Models
{
    public class Game
    {
        public PokemonPool PokemonPool;
        public Timer RoundTimer = new();
        public GameSettings GameSettings;

        public Game(GameSettings gameSettings)
        {
            PokemonPool = new PokemonPool();
            GameSettings = gameSettings;
            RoundTimer.SetDuration(GameSettings.RoundTime);
        }
    }
}