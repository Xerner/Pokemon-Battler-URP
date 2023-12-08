using PokeBattler.Common.Models.Interfaces;
using PokeBattler.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace PokeBattler.Server.Extensions;

public static class TrainersPokemonExtensions
{
    /// <summary>
    /// Adds a Pokemon to the Trainer's active Pokemon if there is room on the bench, or a species is about to evolve
    /// </summary>
    /// <param name="pokemon">The Pokemon to attempt to add to the bench</param>
    /// <param name="bench">The bench spot to add it to</param>
    /// <returns>The PokemonBehaviour instance if the Pokemon was added successfully</returns>
    public static Pokemon Add(this TrainersPokemon trainersPokemon, Pokemon pokemon, IPokeContainer bench)
    {
        bool evolving = trainersPokemon.IsAboutToEvolve(pokemon);
        if (bench == null && !evolving)
        {
            return null; // no fucking room
                         // evolve and/or set the PokemonBehaviour inside the container
        }
        //PokemonBehaviour pokemonBehaviour = PokemonBehaviour.Spawn(pokemon);
        trainersPokemon.Evolve(pokemon);
        // Create it to the Trainers ActivePokemon dictionary
        if (!trainersPokemon.ActivePokemon.ContainsKey(pokemon.name))
        {
            trainersPokemon.ActivePokemon.Add(pokemon.name, new List<Pokemon>());
        }
        trainersPokemon.ActivePokemon[pokemon.name].Add(pokemon);
        //pokemonBehaviour.OnDestroyed += R;
        return pokemon;
    }

    public static bool IsAboutToEvolve(this TrainersPokemon trainersPokemon, Pokemon pokemon)
    {
        var hasPokemon = trainersPokemon.ActivePokemon.ContainsKey(pokemon.name);
        if (!hasPokemon)
        {
            return false;
        }
        var isNotLastEvolution = pokemon.EvolutionStage < pokemon.Evolutions.Count; // Evolution count is 1-indexed
        var hasEnoughOtherPokemon = trainersPokemon.ActivePokemon[pokemon.name].Count > 1;
        return hasPokemon
            && isNotLastEvolution
            && hasEnoughOtherPokemon;
    }

    public static void RemovePokemon(this TrainersPokemon trainersPokemon, Pokemon pokemon)
    {
        if (!trainersPokemon.ActivePokemon.ContainsKey(pokemon.name))
        {
            return;
        }
        var pokemonList = trainersPokemon.ActivePokemon[pokemon.name];
        if (pokemonList.Count == 0)
        {
            trainersPokemon.ActivePokemon.Remove(pokemon.name);
            return;
        }
        pokemonList.Remove(pokemon);
    }
}
