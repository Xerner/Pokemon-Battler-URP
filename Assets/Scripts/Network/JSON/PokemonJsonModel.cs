using System.Collections.Generic;

/// <summary>
/// This class mirrors the data model of https://pokeapi.co/api/v2/pokemon/1/
/// </summary>
namespace JsonModel {
    public class PokemonJsonModel {
        public List<PokemonAbilities> abilities;
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
        public int order;
        //past_types TODO
        //species TODO
    }

    public class PokemonAbilities {
        public NameAndURL ability;
        public bool is_hidden;
        public int slot;
    }

    public class GameIndex {
        public int game_index;
        public NameAndURL version;
    }

    public class PokemonTypes {
        public int slot;
        public NameAndURL type;
    }

    public class Stats {
        public int base_stat;
        public int effort;
        public NameAndURL stat;
    }

    public class NameAndURL {
        public string name;
        public string url;
    }

    public class PokemonItem {
        public NameAndURL item;
        public List<PokemonItemRarity> version_details;
    }

    public class PokemonItemRarity {
        public int rarity;
        public NameAndURL version;
    }

    public class PokemonSprites {
        public string back_default;
        public string back_female;
        public string back_shiny;
        public string back_shiny_female;
        public string front_default;
        public string front_female;
        public string front_shiny_female;
        //other TODO
    }
}
