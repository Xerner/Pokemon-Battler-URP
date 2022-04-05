using System;
using UnityEngine;
using JsonModel;

[CreateAssetMenu(fileName = "New Pokemon", menuName = "Pokemon/Pokemon")]
public class Pokemon : ScriptableObject {
    public int id;
    public new string name;

    public PokemonStat hp = new PokemonStat() { baseStat = 50, effort = 0 };
    public PokemonStat attack = new PokemonStat() { baseStat = 50, effort = 0 };
    public PokemonStat defense = new PokemonStat() { baseStat = 50, effort = 0 };
    public PokemonStat specialAttack = new PokemonStat() { baseStat = 50, effort = 0 };
    public PokemonStat specialDefense = new PokemonStat() { baseStat = 50, effort = 0 };
    public PokemonStat speed = new PokemonStat() { baseStat = 50, effort = 0 };

    public PokemonAbility Ability;
    private int BaseExperience; // TODO: make use of it, or get rid of it
    private int height; // TODO: make use of it, or get rid of it

    public EPokemonType[] types = new EPokemonType[] { EPokemonType.Normal, EPokemonType.None };

    public Pokemon evolution;
    public Pokemon baseEvolution;
    public int EvolutionStage;
    public int tier;

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
                    pokemon.hp.baseStat = pokeStat.base_stat;
                    pokemon.hp.effort = pokeStat.effort;
                    break;
                case "attack":
                    pokemon.attack.baseStat = pokeStat.base_stat;
                    pokemon.attack.effort = pokeStat.effort;
                    break;
                case "defense":
                    pokemon.defense.baseStat = pokeStat.base_stat;
                    pokemon.defense.effort = pokeStat.effort;
                    break;
                case "special-attack":
                    pokemon.specialAttack.baseStat = pokeStat.base_stat;
                    pokemon.specialAttack.effort = pokeStat.effort;
                    break;
                case "special-defense":
                    pokemon.specialDefense.baseStat = pokeStat.base_stat;
                    pokemon.specialDefense.effort = pokeStat.effort;
                    break;
                case "speed":
                    pokemon.speed.baseStat = pokeStat.base_stat;
                    pokemon.speed.effort = pokeStat.effort;
                    break;
                default:
                    throw new System.Exception("Invalid Pokemon stat name: " + pokeStat.stat.name);
            }
        }
        #endregion

        #region Ability
        PokemonJsonModel.PokemonJsonAbility hiddenAbility;
        if (hasHiddenAbility) {
            hiddenAbility = pokemonJson.abilities.Find((ability) => ability.is_hidden == true);
        } else {
            // Remove the hidden ability from the list
            pokemonJson.abilities = pokemonJson.abilities.FindAll((ability) => ability.is_hidden == false);
            hiddenAbility = pokemonJson.abilities[new System.Random().Next(pokemonJson.abilities.Count)];
        }
        pokemon.Ability = new PokemonAbility() { 
            name = hiddenAbility.ability.name, 
            isHidden = hiddenAbility.is_hidden, 
            slot = hiddenAbility.slot, 
            url = hiddenAbility.ability.url
        };
        #endregion

        #region Types
        pokemon.types[0] = PokemonUtil.StringToType(pokemonJson.types.Find((type) => type.slot == 1).type.name);
        if (pokemonJson.types.Count > 1) pokemon.types[1] = PokemonUtil.StringToType(pokemonJson.types.Find((type) => type.slot == 2).type.name);
        #endregion

        // evolution
        // base evolution
        // evolution stage
        // tier
        // sprite
        // shop sprite?
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

        return pokemon;
    }

    public class PokemonStat {
        public int baseStat;
        public int effort;
    }

    public class PokemonAbility {
        public string name;
        public string url;
        public bool isHidden;
        public int slot;
    }
}
