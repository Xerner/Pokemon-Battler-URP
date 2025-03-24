using System.Collections.Generic;

/// <summary>This class mirrors the data model of https://pokeapi.co/api/v2/pokemon/1/</summary>
namespace AutoChess.Contracts.Models.Json
{
    public class PokemonJson
    {
        public List<Ability> abilities = [];
        public List<PokemonTypes> types = [];
        public List<Stats> stats = [];
        public int base_experience = 0;
        public List<NameAndURL> form = [];
        public List<GameIndex> game_indices = [];
        public int height = 0;
        public List<PokemonItem> held_items = [];
        public int id = 0;
        public bool is_default = false;
        public string location_area_encounters = "";
        //moves TODO
        public string name = "";
        public NameAndURL species = new();
        //past_types TODO
        //species TODO
        public PokemonSpriteWithVersions sprites = new();
    }
}
