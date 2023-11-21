using Poke.Unity;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Poke.Core {
    /// <summary>
    /// A class representing the Pokemon a Trainer has active in a game
    /// </summary>
    public class TrainersPokemon
    {
        Dictionary<string, List<PokemonBehaviour>> activePokemon = new Dictionary<string, List<PokemonBehaviour>>();

        /// <summary>
        /// A list of Pokemon that are active in the game, accessed by their species name
        /// </summary>
        public Dictionary<string, List<PokemonBehaviour>> ActivePokemon { get; }

        /// <summary>
        /// Adds a Pokemon to the Trainer's active Pokemon if there is room on the bench, or a species is about to evolve
        /// </summary>
        /// <param name="pokemon">The Pokemon to attempt to add to the bench</param>
        /// <param name="bench">The bench spot to add it to</param>
        /// <returns>The PokemonBehaviour instance if the Pokemon was added successfully</returns>
        public async Task<PokemonBehaviour> Add(Pokemon pokemon, BenchBehaviour bench)
        {
            bool evolving = IsAboutToEvolve(pokemon);
            if (bench == null && !evolving)
            {
                return null; // no fucking room
                             // evolve and/or set the PokemonBehaviour inside the container
            }
            PokemonBehaviour pokemonBehaviour = PokemonBehaviour.Spawn(pokemon);
            await Evolve(pokemonBehaviour);
            // Add it to the Trainers ActivePokemon dictionary
            if (!activePokemon.ContainsKey(pokemonBehaviour.pokemon.name))
            {
                activePokemon.Add(pokemonBehaviour.pokemon.name, new());
            }
            activePokemon[pokemonBehaviour.pokemon.name].Add(pokemonBehaviour);
            pokemonBehaviour.OnDestroyed += RemovePokemon;
            return pokemonBehaviour;
        }

        /// <summary>
        /// Recursively evolve the pokemon until it can't no more
        /// </summary>
        async Task Evolve(PokemonBehaviour pokemon)
        {
            bool evolving = IsAboutToEvolve(pokemon);
            if (!evolving) 
            {
                return;
            }
            // Destroy all other instances of this Pokemon
            foreach (PokemonBehaviour otherPokemon in activePokemon[pokemon.pokemon.name])
            {
                if (pokemon != otherPokemon)
                {
                    Object.Destroy(otherPokemon.gameObject);
                }
            }
            activePokemon.Remove(pokemon.pokemon.name);
            // Evooooolve
            await pokemon.Evolve();
            await Evolve(pokemon);
        }

        public bool IsAboutToEvolve(PokemonBehaviour pokemon) => IsAboutToEvolve(pokemon.pokemon);
        public bool IsAboutToEvolve(Pokemon pokemon)
        {
            var hasPokemon = activePokemon.ContainsKey(pokemon.name);
            if (!hasPokemon)
            {
                return false;
            }
            var isNotLastEvolution = pokemon.EvolutionStage < pokemon.Evolutions.Count; // Evolution count is 1-indexed
            var hasEnoughOtherPokemon = activePokemon[pokemon.name].Count > 1;
            return hasPokemon
                && isNotLastEvolution
                && hasEnoughOtherPokemon;
        }

        void RemovePokemon(PokemonBehaviour pokemon)
        {
            if (!activePokemon.ContainsKey(pokemon.name))
            {
                return;
            }
            var pokemonList = activePokemon[pokemon.name];
            if (pokemonList.Count == 0)
            {
                activePokemon.Remove(pokemon.name);
                return;
            }
            pokemonList.Remove(pokemon);
        }
    }
}
