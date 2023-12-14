using System.Collections.Generic;

namespace PokeBattler.Common.Models.Json
{
    public class EvolutionChainLink
    {
        public PokemonSpecies species;
        public List<EvolutionChainLink> evolves_to;
    }
}
