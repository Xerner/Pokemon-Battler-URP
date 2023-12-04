using PokeBattler.Common.Models;
using PokeBattler.Common.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokeBattler.Server.Models {
    /// <summary>
    /// A class representing the Pokemon a Trainer has active in a game
    /// </summary>
    public class TrainersPokemon
    {
        Dictionary<string, List<Pokemon>> activePokemon = new Dictionary<string, List<Pokemon>>();

        /// <summary>
        /// A list of Pokemon that are active in the game, accessed by their species name
        /// </summary>
        public Dictionary<string, List<Pokemon>> ActivePokemon { get; }

        /// <summary>
        /// Adds a Pokemon to the Trainer's active Pokemon if there is room on the bench, or a species is about to evolve
        /// </summary>
        /// <param name="pokemon">The Pokemon to attempt to add to the bench</param>
        /// <param name="bench">The bench spot to add it to</param>
        /// <returns>The PokemonBehaviour instance if the Pokemon was added successfully</returns>
        public async Task<Pokemon> Add(Pokemon pokemon, IPokeContainer bench)
        {
            bool evolving = IsAboutToEvolve(pokemon);
            if (bench == null && !evolving)
            {
                return null; // no fucking room
                             // evolve and/or set the PokemonBehaviour inside the container
            }
            //PokemonBehaviour pokemonBehaviour = PokemonBehaviour.Spawn(pokemon);
            await Evolve(pokemon);
            // Create it to the Trainers ActivePokemon dictionary
            if (!activePokemon.ContainsKey(pokemon.name))
            {
                activePokemon.Add(pokemon.name, new List<Pokemon>());
            }
            activePokemon[pokemon.name].Add(pokemon);
            //pokemonBehaviour.OnDestroyed += R;
            return pokemon;
        }

        /// <summary>
        /// Recursively evolve the pokemon until it can't no more
        /// </summary>
        async Task Evolve(Pokemon pokemon)
        {
            bool evolving = IsAboutToEvolve(pokemon);
            if (!evolving) 
            {
                return;
            }
            // Destroy all other instances of this Pokemon
            foreach (Pokemon otherPokemon in activePokemon[pokemon.name])
            {
                if (pokemon != otherPokemon)
                {
                    Object.Destroy(otherPokemon.Id);
                }
            }
            activePokemon.Remove(pokemon.name);
            // Evooooolve
            await pokemon.Evolve();
            await Evolve(pokemon);
        }

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

        void RemovePokemon(Pokemon pokemon)
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
