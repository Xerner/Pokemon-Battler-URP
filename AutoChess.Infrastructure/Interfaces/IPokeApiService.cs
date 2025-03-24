using AutoChess.Contracts.Models;

namespace AutoChess.Contracts.ExternalApi;

public interface IPokeApiService
{
    Task<Pokemon> AddPokemonToCache(string json);
    Task<Pokemon> GetEvolution(Pokemon pokemon);
    Task<Pokemon> GetPokemon(int pokemonId);
    Task<Pokemon> GetPokemon(string pokemonName);
    Task<IEnumerable<Pokemon>> InitializeListOfPokemon(IEnumerable<string> pokemonNames, Action<IEnumerable<Pokemon>> callback = null);
}