using PokeBattler.Common.Models;
using PokeBattler.Server.Models;

namespace PokeBattler.Server.Extensions;

public static class TrainersPokemonExtensions
{
    public static bool IsAboutToEvolve(this TrainersPokemon trainersPokemon, Pokemon pokemon)
    {
        var hasPokemon = trainersPokemon.ContainsKey(pokemon.name);
        if (!hasPokemon)
        {
            return false;
        }
        var isNotLastEvolution = pokemon.EvolutionStage < pokemon.Evolutions.Count; // Evolution count is 1-indexed
        var hasEnoughOtherPokemon = trainersPokemon[pokemon.name].Count > 1;
        return hasPokemon
            && isNotLastEvolution
            && hasEnoughOtherPokemon;
    }

    public static void RemovePokemon(this TrainersPokemon trainersPokemon, Pokemon pokemon)
    {
        if (!trainersPokemon.ContainsKey(pokemon.name))
        {
            return;
        }
        var pokemonList = trainersPokemon[pokemon.name];
        if (pokemonList.Count == 0)
        {
            trainersPokemon.Remove(pokemon.name);
            return;
        }
        pokemonList.Remove(pokemon);
    }
}
