using System.Collections.Generic;

/// <summary>This class mirrors the data model of https://pokeapi.co/api/v2/pokemon/1/</summary>
namespace PokeBattler.Common.Models.Json
{
    public class PokemonJson
    {
        public List<Ability> abilities;
        public List<PokemonTypes> types;
        public List<Stats> stats;
        public int base_experience;
        public List<NameAndURL> form;
        public List<GameIndex> game_indices;
        public int height;
        public List<PokemonItem> held_items;
        public int id;
        public bool is_default;
        public string location_area_encounters;
        //moves TODO
        public string name;
        public NameAndURL species;
        //past_types TODO
        //species TODO
        public PokemonSpriteWithVersions sprites;
    }
}
