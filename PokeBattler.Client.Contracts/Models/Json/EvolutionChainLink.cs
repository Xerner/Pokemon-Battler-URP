using System.Collections.Generic;

namespace AutoChess.Contracts.Models.Json
{
    public class EvolutionChainLink
    {
        public PokemonSpecies species;
        public List<EvolutionChainLink> evolves_to;
    }
}
