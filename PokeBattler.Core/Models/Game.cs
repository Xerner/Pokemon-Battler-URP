namespace PokeBattler.Common.Models
{
    public class Game
    {
        public PokemonPool PokemonPool;
        public Timer RoundTimer = new();
        public GameSettings GameSettings;

        public Game(GameSettings gameSettings)
        {
            GameSettings = gameSettings;
            PokemonPool = new PokemonPool();
            Debug2.Log($"Initialized PokemonPool with {Pokemon.CachedPokemon.Count} different Pokemon", LogLevel.Detailed);
            RoundTimer.SetDuration(GameSettings.RoundTime);
        }
    }
}