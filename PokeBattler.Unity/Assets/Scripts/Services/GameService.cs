using PokeBattler.Models;
using PokeBattler.Services;
using System;
using System.Collections.Generic;

namespace PokeBattler.Core
{
    public class GameService
    {
        private readonly GameSettings defaultGameSettings;
        public Game Game;
        public Action<Game> OnGameCreated;
        public Action<PokemonPool> OnPokemonDataLoaded;

        private readonly TrainersService trainersService;

        public GameService(GameSettings defaultGameSettings, TrainersService trainersService)
        {
            this.defaultGameSettings = defaultGameSettings;
            this.trainersService = trainersService;
        }

        public Game CreateGame()
        {
            var game = new Game()
            {
                GameSettings = defaultGameSettings
            };
            game.RoundTimer.SetDuration(game.GameSettings.RoundTime);
            Pokemon.InitializeListOfPokemon(new List<string>() { "bulbasaur", "squirtle", "charmander", "magnemite", "abra" }).Wait();
            game.PokemonPool = new PokemonPool(trainersService);
            OnPokemonDataLoaded(game.PokemonPool);

            Game = game;
            OnGameCreated.Invoke(game);
            return game;
        }

        public Trainer CreateTrainer(Account hostAccount)
        {
            Trainer trainer = new Trainer(hostAccount);
            trainersService.Add(trainer);
            return trainer;
        }
    }
}
