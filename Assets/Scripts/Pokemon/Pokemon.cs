﻿using System;
using UnityEngine;
using JsonModel;
using static JsonModel.PokemonJsonModel;
using Newtonsoft.Json;
using System.Collections.Generic;

[Serializable]
public class Pokemon {

    public static Dictionary<string, Pokemon> CachedPokemon = new Dictionary<string, Pokemon>();
    public static Dictionary<int, List<Pokemon>> TierToPokemonList = new Dictionary<int, List<Pokemon>>() {
        { 1, new List<Pokemon>() },
        { 2, new List<Pokemon>() },
        { 3, new List<Pokemon>() },
        { 4, new List<Pokemon>() },
        { 5, new List<Pokemon>() }
    };

    public int id;
    public string name;
    private string correctedName;
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

    public List<string> Evolutions;
    public int EvolutionStage;

    public Sprite Sprite;
    public Sprite ShopSprite;

    public static void GetPokemonFromAPI(string idOrName, Action<Pokemon> onSuccess = null) {
        string correctedName = correctPokemonName(idOrName);
        // if we already fetched this pokemons data, do not fetch it again
        if (CachedPokemon.ContainsKey(correctedName)) {
            Debug.Log("Fetched from cache: " + correctedName);
            onSuccess?.Invoke(CachedPokemon[correctedName]);
            return;
        } else {
            string url;
            if (correctedName == "nidoran") url = $"https://pokeapi.co/api/v2/pokemon/nidoran-m/";
            else url = $"https://pokeapi.co/api/v2/pokemon/{correctedName}/";
            Debug2.Log("Fetching pokemon: " + idOrName, LogLevel.Detailed);
            WebRequests.Get<string>(
                url,
                (error) => Debug.LogError($"Failed to fetch Pokemon ({idOrName})\n" + error),
                (json) => PokemonFromJson(json, false, (pokemon) => {
                    Debug.Log("Fetched: " + pokemon.name);
                    Debug2.Log($"Adding {pokemon.name} to the cached Pokemon", LogLevel.Detailed);
                    // Do not simplify name. It is needed like this for watching its variable value because asynchronous debugging is a bitch
                    Pokemon.CachedPokemon.Add(pokemon.correctedName, pokemon);
                    Pokemon.TierToPokemonList[pokemon.tier].Add(pokemon);
                    onSuccess?.Invoke(pokemon);
                })
            );
        }
    }

    private static string correctPokemonName(string pokemonName) {
        switch (pokemonName) {
            case "nidoran-m":
                return "nidoran";
            default:
                return pokemonName.ToLower();
        }
    }

    public static void InitializeAllPokemon() {
        Debug2.Log("Initializing all Pokemon");
        string[] pokemonNames = Enum.GetNames(typeof(EPokemonName));
        for (int i = 0; i < pokemonNames.Length; i++) pokemonNames[i] = pokemonNames[i].ToLower();
        List<string> pokemonToInitialize = new List<string>();
        foreach (var name in pokemonNames) if (!CachedPokemon.ContainsKey(name)) pokemonToInitialize.Add(name);
        InitializeListOfPokemon(pokemonToInitialize);
    }

    /// <summary>Recursive</summary>
    public static void InitializeListOfPokemon(List<string> pokemonNames) {
        Debug2.Log($"Initializing {pokemonNames.Count} Pokemon");
        Debug2.Log(string.Join(",", pokemonNames.ToArray()), LogLevel.Detailed);
        foreach (var name in pokemonNames) {
            GetPokemonFromAPI(name);
        }
        //if (index == pokemonNames.Count) {
        //    Debug2.Log($"Done fetching {pokemonNames.Count} Pokemon");
        //    return;
        //} else {
        //    GetPokemonFromAPI(
        //        pokemonName,
        //        (pokemon) => InitializeListOfPokemon(pokemonNames, index + 1)
        //    );
        //}
    }

    /// <summary>
    /// Builds a Pokemon from json
    /// </summary>
    /// <param name="json">json returned from the Poke API</param>
    public static void PokemonFromJson(string json, bool hasHiddenAbility, Action<Pokemon> onSuccess) {
        var pokemonJson = FromJson(json);

        Pokemon pokemon = new Pokemon();
        pokemon.id = pokemonJson.id;
        pokemon.correctedName = correctPokemonName(pokemonJson.name);
        pokemon.name = pokemon.correctedName.ToProper();
        pokemon.BaseExperience = pokemonJson.base_experience;
        pokemon.height = pokemonJson.height;
        if (PokemonConstants.enumToTier.ContainsKey(pokemon.id)) pokemon.tier = PokemonConstants.enumToTier[pokemon.id];
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

        Debug2.Log("Fetching pokemon ability: " + pokemon.name, LogLevel.Detailed);
        WebRequests.Get<string>(
            ability.ability.url,
            (error) => Debug.LogError(error),
            (jsonAbility) => {
                PokemonJsonAbilityWithDescription ability = JsonConvert.DeserializeObject<PokemonJsonAbilityWithDescription>(jsonAbility);
                PokemonJsonAbilityEffect effect = ability.effect_entries.Find((effect) => effect.language.name == "en");
                pokemon.Ability.description = effect.short_effect;
                pokemon.Ability.longDescription = effect.effect;

                #region Species
                Debug2.Log("Fetching pokemon species: " + pokemon.name, LogLevel.Detailed);
                WebRequests.Get<string>(
                    pokemonJson.species.url,
                    (error) => Debug.LogError(error),
                    (jsonSpecies) => {
                        JsonPokemonSpecies species = JsonConvert.DeserializeObject<JsonPokemonSpecies>(jsonSpecies);
                        #region Evolution Chain
                        Debug2.Log("Fetching pokemon evolution chain: " + pokemon.name, LogLevel.Detailed);
                        WebRequests.Get<string>(
                            species.evolution_chain.url,
                            (error) => Debug.LogError(error),
                            (jsonEvolutionChain) => {
                                JsonEvolutionChain evolutionChain = JsonConvert.DeserializeObject<JsonEvolutionChain>(jsonEvolutionChain);
                                pokemon.Evolutions = evolutionChain.GetEvolutions();
                                pokemon.EvolutionStage = evolutionChain.GetEvolutionStage(pokemonJson.name);

                                #region Sprites
                                Debug2.Log("Fetching pokemon sprites: " + pokemon.name, LogLevel.Detailed);
                                WebRequests.Get<Texture2D>(
                                    pokemonJson.sprites.versions["generation-v"]["black-white"].front_default,
                                    (error) => Debug.LogError(error),
                                    (texture) => {
                                        pokemon.Sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 1f);
                                        pokemon.ShopSprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 1f);
                                        Debug2.Log("Fetching pokemon sprites: " + pokemon.name, LogLevel.Detailed);
                                        onSuccess?.Invoke(pokemon);
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
    }

    public string EvolutionsToString() {
        string str = "";
        for (int i = 0; i < Evolutions.Count; i++) {
            if (i == Evolutions.Count - 1) str += Evolutions[i];
            else str += Evolutions[i] + " → ";
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

    [Serializable]
    public class PokemonStat {
        public int baseStat;
        public int effort;
    }

    [Serializable]
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
