namespace PokeBattler.Common.Models.Json
{
    public class PokemonSpecies
    {
        public int id;
        public string name;
        public JsonPokemonSpeciesEvolutionChain evolution_chain;

        public class JsonPokemonSpeciesEvolutionChain
        {
            public string name;
            public string url;
        }

        // TODO: test this function
        public int GetID() => int.Parse(
            evolution_chain.url.Substring(
                evolution_chain.url.Length - evolution_chain.url.Substring(
                    evolution_chain.url.Length - 1
                ).IndexOf("/")
            )
        );
    }
}
