using Microsoft.Extensions.Logging;
using PokeBattler.Common.Models;
using PokeBattler.Server.Services;
using System;

namespace PokeBattler.Client.Services;

public interface IGameService
{
    public Game Game { get; }
    public Game CreateGame();
}

public class GameService(ILogger<GameService> logger, 
                         IPokemonPoolService pokemonPools,
                         IPokeApiService pokemonService, 
                         GameSettings defaultGameSettings) : IGameService
{
    public Game Game { get; private set; }

    public Game CreateGame()
    {
        // This should be done differently when I have time. It should load pokemon on
        // game launch based on a ScriptableObject or something, and then cache them somehow
        // for reuse on next game launch
        Game = new(defaultGameSettings);
        pokemonPools.Initialize(Game.PokemonPool);
        logger.LogInformation($"Initialized PokemonPoolService with {pokemonService.CachedPokemon.Count} different Pokemon");
        return Game;
    }
}
