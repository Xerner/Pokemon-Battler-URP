using System;
using UnityEngine;
using JsonModel;
using static JsonModel.PokemonJsonModel;
using Newtonsoft.Json;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Pokemon", menuName = "Pokemon/Pokemon")]
public class Pokemon : ScriptableObject {
    public int id;
    public new string name;

    public int tier;

    public PokemonStat Hp = new PokemonStat() { baseStat = 50, effort = 0 };
    public PokemonStat Attack = new PokemonStat() { baseStat = 50, effort = 0 };
    public PokemonStat Defense = new PokemonStat() { baseStat = 50, effort = 0 };
    public PokemonStat SpecialAttack = new PokemonStat() { baseStat = 50, effort = 0 };
    public PokemonStat SpecialDefense = new PokemonStat() { baseStat = 50, effort = 0 };
    public PokemonStat Speed = new PokemonStat() { baseStat = 50, effort = 0 };

    public PokemonAbility Ability;
    private int BaseExperience; // TODO: make use of it, or get rid of it
    private int height; // TODO: make use of it, or get rid of it

    public EPokemonType[] types = new EPokemonType[] { EPokemonType.Normal, EPokemonType.None };

    public List<string> evolutions;
    public int EvolutionStage;

    public Sprite sprite;
    public Sprite shopSprite;

    /// <summary>
    /// Returns a valid Pokemon name for querying Poke API from the given Pokemon name
    /// </summary>
    /// <param name="name">A possibly valid Pokemon name</param>
    /// <returns>A valid Pokemon name, or an empty string if the given name was invalid</returns>
    public static string GetValidPokemonName(string name) {
        foreach (Enum pokeName in Enum.GetValues(typeof(EPokemonName))) {
            if (name.Trim().ToLower() == pokeName.ToString().ToLower()) return pokeName.ToString();
        }
        return "";
    }

    public static void GetPokemonFromAPI(string idOrName, Action<Pokemon> onSuccess) {
        WebRequests.Get<string>(
            $"https://pokeapi.co/api/v2/pokemon/{idOrName}/", 
            (error) => Debug.LogError(error), 
            (json) => PokemonFromJson(json, false, (pokemon) => onSuccess(pokemon))
        );
    }

    /// <summary>
    /// Builds a Pokemon from json
    /// </summary>
    /// <param name="json">json returned from the Poke API</param>
    public static Pokemon PokemonFromJson(string json, bool hasHiddenAbility, Action<Pokemon> onSuccess) {
        Pokemon pokemon = CreateInstance<Pokemon>();
        var pokemonJson = PokemonJsonModel.FromJson(json);
        pokemon.id = pokemonJson.id;
        pokemon.name = pokemonJson.name.ToProper();
        pokemon.BaseExperience = pokemonJson.base_experience;
        pokemon.height = pokemonJson.height;

        #region Stats
        foreach (var pokeStat in pokemonJson.stats) {
            switch (pokeStat.stat.name) {
                case "hp":
                    pokemon.Hp.baseStat = pokeStat.base_stat;
                    pokemon.Hp.effort = pokeStat.effort;
                    break;
                case "attack":
                    pokemon.Attack.baseStat = pokeStat.base_stat;
                    pokemon.Attack.effort = pokeStat.effort;
                    break;
                case "defense":
                    pokemon.Defense.baseStat = pokeStat.base_stat;
                    pokemon.Defense.effort = pokeStat.effort;
                    break;
                case "special-attack":
                    pokemon.SpecialAttack.baseStat = pokeStat.base_stat;
                    pokemon.SpecialAttack.effort = pokeStat.effort;
                    break;
                case "special-defense":
                    pokemon.SpecialDefense.baseStat = pokeStat.base_stat;
                    pokemon.SpecialDefense.effort = pokeStat.effort;
                    break;
                case "speed":
                    pokemon.Speed.baseStat = pokeStat.base_stat;
                    pokemon.Speed.effort = pokeStat.effort;
                    break;
                default:
                    throw new System.Exception("Invalid Pokemon stat name: " + pokeStat.stat.name);
            }
        }
        #endregion

        #region Types
        pokemon.types[0] = PokemonUtil.StringToType(pokemonJson.types.Find((type) => type.slot == 1).type.name);
        if (pokemonJson.types.Count > 1) pokemon.types[1] = PokemonUtil.StringToType(pokemonJson.types.Find((type) => type.slot == 2).type.name);
        #endregion

        #region Ability

        PokemonJsonAbility ability;
        if (hasHiddenAbility) {
            ability = pokemonJson.abilities.Find((ability) => ability.is_hidden == true);
        } else {
            // Remove the hidden ability from the list
            pokemonJson.abilities = pokemonJson.abilities.FindAll((ability) => ability.is_hidden == false);
            ability = pokemonJson.abilities[new System.Random().Next(pokemonJson.abilities.Count)];
        }
        pokemon.Ability = new PokemonAbility() { 
            name = ability.ability.name.ToProper().Replace("-", " "),
            isHidden = ability.is_hidden, 
            slot = ability.slot, 
            url = ability.ability.url
        };

        WebRequests.Get<string>(
            ability.ability.url,
            (error) => Debug.LogError(error),
            (jsonAbility) => {
                PokemonJsonAbilityWithDescription ability = JsonConvert.DeserializeObject<PokemonJsonAbilityWithDescription>(jsonAbility);
                PokemonJsonAbilityEffect effect = ability.effect_entries.Find((effect) => effect.language.name == "en");
                pokemon.Ability.description = effect.short_effect;
                pokemon.Ability.longDescription = effect.effect;
                onSuccess.Invoke(pokemon);

                #region Species
                WebRequests.Get<string>(
                    pokemonJson.species.url,
                    (error) => Debug.LogError(error),
                    (jsonSpecies) => { 
                        JsonPokemonSpecies species = JsonConvert.DeserializeObject<JsonPokemonSpecies>(jsonSpecies);
                        #region Evolution Chain
                        WebRequests.Get<string>(
                            species.evolution_chain.url,
                            (error) => Debug.LogError(error),
                            (jsonEvolutionChain) => {
                                JsonEvolutionChain evolutionChain = JsonConvert.DeserializeObject<JsonEvolutionChain>(jsonEvolutionChain);
                                pokemon.evolutions = evolutionChain.GetEvolutions();
                                pokemon.EvolutionStage = evolutionChain.GetEvolutionStage(pokemon.name);
                                
                                #region Sprites
                                WebRequests.Get<Texture2D>(
                                    pokemonJson.sprites.versions["generation-v"]["black-white"].front_default,
                                    (error) => Debug.LogError(error),
                                    (texture) => {
                                        pokemon.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 1f);
                                        onSuccess.Invoke(pokemon);
                                    }
                                );
                                #endregion
                            }
                        );
                        #endregion
                    }
                );
                #endregion
            }
        );

        #endregion

        return pokemon;
    }

    public string EvolutionsToString() {
        string str = "";
        for (int i = 0; i < evolutions.Count; i++) {
            if (i == evolutions.Count - 1) str += evolutions[i];
            else str += evolutions[i] + " → ";
        }
        return str;
    }

    public string TypeToString() {
        string str = "";
        for (int i = 0; i < types.Length; i++) {
            if (types[i] == EPokemonType.None) continue;
            if (i == types.Length - 1) str += types[i];
            else str += types[i] + "    ";
        }
        return str;
    }

    #region Helper Classes

    public class PokemonStat {
        public int baseStat;
        public int effort;
    }

    public class PokemonAbility {
        public string name;
        public string description;
        public string longDescription;
        public string url;
        public bool isHidden;
        public int slot;
    }

    #endregion
}
