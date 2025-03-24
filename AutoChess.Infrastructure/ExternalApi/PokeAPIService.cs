using Microsoft.Extensions.Logging;
using AutoChess.Infrastructure.Services;
using AutoChess.Contracts.Models;
using AutoChess.Contracts.ExternalApi;
using AutoChess.Infrastructure.Interfaces;

namespace AutoChess.Infrastructure.ExternalApi;

public class PokeAPIService(ILogger<PokeAPIService> logger, HttpService http, IJsonService jsonService) : IPokeApiService
{
    private static readonly string[] stringArray = ["bulbasaur", "squirtle", "charmander", "magnemite", "abra"];
    public Dictionary<int, Pokemon> CachedPokemon { get; set; } = [];

    public async Task<Pokemon> GetEvolution(Pokemon pokemon)
    {
        if (pokemon.EvolutionStage >= pokemon.Evolutions.Count)
        {
            return pokemon;
        }
        var evolution = pokemon.Evolutions[pokemon.EvolutionStage];
        if (evolution == null)
        {
            return pokemon;
        }
        return await GetPokemon(evolution);
    }

    public async Task<Pokemon> GetPokemon(int pokemonId)
    {
        return await GetPokemon(pokemonId.ToString());
    }

    public async Task<Pokemon> GetPokemon(string pokemonName)
    {
        string url = $"https://pokeapi.co/api/v2/pokemon/{pokemonName}/";
        logger.LogInformation("Fetching pokemon: " + pokemonName);
        string json = "";
        try
        {
            json = await http.GetAsync(url);
        }
        catch
        {
            logger.LogError($"Failed to fetch Pokemon ({pokemonName})\n");
        }
        return await AddPokemonToCache(json);
    }

    public async Task<IEnumerable<Pokemon>> InitializeListOfPokemon(IEnumerable<string> pokemonNames, Action<IEnumerable<Pokemon>> callback = null)
    {
        logger.LogInformation($"Fetching {pokemonNames.Count()} Pokemon");
        logger.LogInformation(string.Join(",", pokemonNames.ToArray()));
        var pokemon = new List<Pokemon>();
        var tasks = new List<Task<Pokemon>>();
        foreach (var name in pokemonNames)
        {
            tasks.Add(GetPokemon(name));
        }
        await Task.WhenAll(tasks);
        foreach (var task in tasks)
        {
            pokemon.Add(task.Result);
        }
        if (callback != null)
            callback(pokemon);
        return pokemon;
    }

    public async Task<Pokemon> AddPokemonToCache(string json)
    {
        var pokemon = await jsonService.PokemonFromJson(json, false);
        logger.LogInformation($"Adding {pokemon.Name} to the cached Pokemon");
        // Do not simplify name. It is needed like this for watching its variable value because asynchronous debugging is a bitch
        CachedPokemon.Add(pokemon.PokeId, pokemon);
        return pokemon;
    }
}
