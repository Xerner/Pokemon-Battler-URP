using Newtonsoft.Json;
using System.Collections.Generic;

/// <summary>
/// This class mirrors the data model of https://pokeapi.co/api/v2/pokemon/1/
/// </summary>
namespace JsonModel {
    public class PokemonJsonModel {
        public List<PokemonJsonAbility> abilities;
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
        public PokemonSpriteWithVersions sprites;

        public static PokemonJsonModel FromJson(string json) {
            return JsonConvert.DeserializeObject<PokemonJsonModel>(json);
        }

        #region subclasses

        public class PokemonJsonAbility {
            public NameAndURL ability;
            public bool is_hidden;
            public int slot;
        }

        public class PokemonJsonAbilityWithDescription {
            public List<PokemonJsonAbilityEffect> effect_entries;
        }

        public class PokemonJsonAbilityEffect {
            public string effect;
            public NameAndURL language;
            public string short_effect;
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

        public class PokemonSprite {
            public string back_default;
            public string back_female;
            public string back_shiny;
            public string back_shiny_female;
            public string front_default;
            public string front_female;
            public string front_shiny;
            public string front_shiny_female;
            // maybe include the "other" sprites from the JSON
        }

        public class PokemonSpriteWithVersions : PokemonSprite {
            public Dictionary<string, Dictionary<string, PokemonSpriteBlackWhite>> versions;
        }

        public class PokemonSpriteBlackWhite : PokemonSprite {
            public PokemonSprite animated;
        }

        #endregion
    }
}
