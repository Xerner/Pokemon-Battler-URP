using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class mirrors the data model of https://pokeapi.co/api/v2/pokemon/1/
/// </summary>
namespace Poke.Model {
    public class PokemonJson {
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

        public static PokemonJson FromJson(string json) {
            return JsonConvert.DeserializeObject<PokemonJson>(json);
        }

        #region subclasses

        public class Ability {
            public NameAndURL ability;
            public bool is_hidden;
            public int slot;
        }

        public class AbilityWithDescription {
            public List<AbilityEffect> effect_entries;
        }

        public class AbilityEffect {
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
            public List<ItemRarity> version_details;
        }

        public class ItemRarity {
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

    public class JsonPokemonSpecies {
        public int id;
        public string name;
        public JsonPokemonSpeciesEvolutionChain evolution_chain;

        public class JsonPokemonSpeciesEvolutionChain {
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

    public class JsonEvolutionChain {
        public int id;
        public JsonEvolutionChainLink chain;

        #region functions

        /// <summary>
        /// Recursive function that returns a List of names of all Pokemon in the evolution chain
        /// </summary>
        public List<string> GetEvolutions() => calcEvolutions(chain);

        /// <summary>
        /// Internal function for calculating a List of names of all Pokemon in the evolution chain
        /// </summary>
        private List<string> calcEvolutions(JsonEvolutionChainLink chainLink, List<string> evolutions = null) {
            if (evolutions == null) evolutions = new List<string>();
            evolutions.Add(chainLink.species.name.ToProper());
            if (chainLink.evolves_to.Count == 0) {
                return evolutions;
            } else {
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
        private int calcEvolutionStage(string pokemonName, JsonEvolutionChainLink chainLink, int evolutionStage = 1) {
            if (chainLink == null || chainLink.species == null) {
                Debug.LogError("Could not calculate evolution stage. Invalid Pokemon name: " + pokemonName);
                return -1;
            } else if (chainLink.species.name == pokemonName) {
                return evolutionStage;
            } else if (chainLink.evolves_to.Count == 0) { // count == 0 and name was never matched
                return evolutionStage;
            } else {
                return calcEvolutionStage(pokemonName, chainLink.evolves_to[0], evolutionStage + 1);
            }
        }

        #endregion
    }

    public class JsonEvolutionChainLink {
        public JsonPokemonSpecies species;
        public List<JsonEvolutionChainLink> evolves_to;
    }
}
