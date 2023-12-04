using PokeBattler.Common;
using PokeBattler.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokeBattler.Client.Services
{
    public interface IGameService
    {
        public Game Game { get; }
        public Action<Game> OnGameCreated { get; set; }
        public Action<PokemonPool> OnPokemonDataLoaded { get; set; }
        public Game CreateGame();
    }

    public class GameService : IGameService
    {
        private readonly GameSettingsSO defaultGameSettings;
        public Game Game { get; private set; }
        public Action<Game> OnGameCreated { get; set; }
        public Action<PokemonPool> OnPokemonDataLoaded { get; set; }

        public GameService(GameSettingsSO defaultGameSettings)
        {
            this.defaultGameSettings = defaultGameSettings;
        }

        public Game CreateGame()
        {
            // This should be done differently when I have time. It should load pokemon on
            // game launch based on a ScriptableObject or something, and then cache them somehow
            // for reuse on next game launch
            Game = new(defaultGameSettings);
            OnPokemonDataLoaded?.Invoke(Game.PokemonPool);
            OnGameCreated?.Invoke(Game);
            return Game;
        }
    }
}
