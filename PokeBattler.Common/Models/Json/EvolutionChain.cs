using System.Collections.Generic;

namespace PokeBattler.Common.Models.Json
{
    public class EvolutionChain
    {
        public int id;
        public EvolutionChainLink chain;

        #region functions

        /// <summary>
        /// Recursive function that returns a List of names of all Pokemon in the evolution chain
        /// </summary>
        public List<string> GetEvolutions() => calcEvolutions(chain);

        /// <summary>
        /// Internal function for calculating a List of names of all Pokemon in the evolution chain
        /// </summary>
        private List<string> calcEvolutions(EvolutionChainLink chainLink, List<string> evolutions = null)
        {
            if (evolutions == null) evolutions = new List<string>();
            evolutions.Add(chainLink.species.name.ToProper());
            if (chainLink.evolves_to.Count == 0)
            {
                return evolutions;
            }
            else
            {
                return calcEvolutions(chainLink.evolves_to[0], evolutions);
            }
        }

        /// <summary>
        /// Recursive function to calculate the evolution stage of the pokemon
        /// </summary>
        /// <returns>The evolution stage of the given pokemon</returns>
        public int GetEvolutionStage(string pokemonName) => calcEvolutionStage(pokemonName, chain);

        /// <summary>
        /// Internal method for recursively calculating a Pokemons evolution stage
        /// </summary>
        private int calcEvolutionStage(string pokemonName, EvolutionChainLink chainLink, int evolutionStage = 1)
        {
            if (chainLink == null || chainLink.species == null)
            {
                //Debug.LogError("Could not calculate evolution stage. Invalid Pokemon name: " + pokemonName);
                return -1;
            }
            else if (chainLink.species.name == pokemonName)
            {
                return evolutionStage;
            }
            else if (chainLink.evolves_to.Count == 0)
            { // count == 0 and name was never matched
                return evolutionStage;
            }
            else
            {
                return calcEvolutionStage(pokemonName, chainLink.evolves_to[0], evolutionStage + 1);
            }
        }

        #endregion
    }
}
