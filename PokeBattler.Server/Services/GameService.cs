using Microsoft.Extensions.Logging;
using PokeBattler.Common.Models;
using PokeBattler.Server.Services;
using System;

namespace PokeBattler.Client.Services;

public interface IGameService
{
    public Game Game { get; }
    public Action<Game> OnGameCreated { get; set; }
    public Action<PokemonPool> OnPokemonDataLoaded { get; set; }
    public Game CreateGame();
}

public class GameService(ILogger<GameService> logger, 
                         PokemonPoolService pokemonPools, 
                         PokeAPIService pokemonService, 
                         GameSettings defaultGameSettings) : IGameService
{
    public Game Game { get; private set; }
    public Action<Game> OnGameCreated { get; set; }
    public Action<PokemonPool> OnPokemonDataLoaded { get; set; }

    public Game CreateGame()
    {
        // This should be done differently when I have time. It should load pokemon on
        // game launch based on a ScriptableObject or something, and then cache them somehow
        // for reuse on next game launch
        Game = new(defaultGameSettings);
        pokemonPools.InitializeFromCache(Game.PokemonPool);
        OnPokemonDataLoaded?.Invoke(Game.PokemonPool);
        logger.LogInformation($"Initialized PokemonPoolService with {pokemonService.CachedPokemon.Count} different Pokemon", LogLevel.Detailed);
        OnGameCreated?.Invoke(Game);
        return Game;
    }
}
