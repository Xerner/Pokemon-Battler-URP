using PokeBattler.Common.Models.Enums;
using PokeBattler.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using PokeBattler.Common.Models.Json;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace PokeBattler.Server.Services;

public class PokeAPIService
{
    readonly ILogger<PokeAPIService> logger;
    readonly HttpService http;

    public PokeAPIService(ILogger<PokeAPIService> logger, HttpService http)
    {
        this.logger = logger;
        this.http = http;
        InitializeListOfPokemon(new List<string>() { "bulbasaur", "squirtle", "charmander", "magnemite", "abra" });
    }

    public Dictionary<string, Pokemon> CachedPokemon = new Dictionary<string, Pokemon>();
    public Dictionary<int, List<Pokemon>> TierToPokemonList = new Dictionary<int, List<Pokemon>>() {
        { 1, new List<Pokemon>() },
        { 2, new List<Pokemon>() },
        { 3, new List<Pokemon>() },
        { 4, new List<Pokemon>() },
        { 5, new List<Pokemon>() }
    };
    public int TotalPokemon
    {
        get
        {
            return Enum.GetNames(typeof(EPokemonName)).Length;
        }
    }

    public async Task<Pokemon> GetPokemonFromAPI(string idOrName)
    {
        string correctedName = correctPokemonName(idOrName);
        // if we already fetched this TrainerToShopPokemon data, do not fetch it again
        if (CachedPokemon.ContainsKey(correctedName))
        {
            //Debug.Log("Fetched from cache: " + correctedName);
            return CachedPokemon[correctedName];
        }
        else
        {
            string url;
            if (correctedName == "nidoran") url = $"https://pokeapi.co/protocol/v2/pokemon/nidoran-m/";
            else url = $"https://pokeapi.co/protocol/v2/pokemon/{correctedName}/";
            logger.LogInformation("Fetching pokemon: " + idOrName, LogLevel.All);
            string json = "";
            try
            {
                json = await http.GetAsync(url);
            }
            catch
            {
                logger.LogError($"Failed to fetch Pokemon ({idOrName})\n");
            }
            var pokemon = await PokemonFromJson(json, false);
            logger.LogInformation($"Adding {pokemon.name} to the cached Pokemon", LogLevel.Detailed);
            // Do not simplify name. It is needed like this for watching its variable value because asynchronous debugging is a bitch
            CachedPokemon.Add(pokemon.correctedName, pokemon);
            TierToPokemonList[pokemon.tier].Add(pokemon);
            return pokemon;
        }
    }

    private string correctPokemonName(string pokemonName)
    {
        switch (pokemonName)
        {
            case "nidoran-m":
                return "nidoran";
            default:
                return pokemonName.ToLower();
        }
    }

    public async Task<List<Pokemon>> InitializeAllPokemon(Action<List<Pokemon>> callback = null)
    {
        logger.LogInformation("Initializing all Pokemon");
        string[] pokemonNames = Enum.GetNames(typeof(EPokemonName));
        for (int i = 0; i < pokemonNames.Length; i++) pokemonNames[i] = pokemonNames[i].ToLower();
        List<string> pokemonToInitialize = new List<string>();
        foreach (var name in pokemonNames) if (!CachedPokemon.ContainsKey(name)) pokemonToInitialize.Add(name);
        var pokemon = await InitializeListOfPokemon(pokemonToInitialize);
        if (callback != null)
            callback(pokemon);
        return pokemon;
    }

    /// <summary>Recursive</summary>
    public async Task<List<Pokemon>> InitializeListOfPokemon(List<string> pokemonNames, Action<List<Pokemon>> callback = null)
    {
        logger.LogInformation($"Fetching {pokemonNames.Count} Pokemon");
        logger.LogInformation(string.Join(",", pokemonNames.ToArray()), LogLevel.Detailed);
        var pokemon = new List<Pokemon>();
        var tasks = new List<Task<Pokemon>>();
        foreach (var name in pokemonNames)
        {
            tasks.Add(GetPokemonFromAPI(name));
        }
        await Task.WhenAll(tasks);
        foreach (var task in tasks)
        {
            pokemon.Add(task.Result);
        }
        if (callback != null)
            callback(pokemon);
        return pokemon;
    }

    /// <summary>
    /// Builds a Pokemon from json
    /// </summary>
    /// <param name="json">json returned from the Poke API</param>
    public async Task<Pokemon> PokemonFromJson(string json, bool hasHiddenAbility)
    {
        // TODO: split up the different HTTP calls in this method into their own JSON deserialization classes
        var pokemonJson = JsonConvert.DeserializeObject<PokemonJson>(json);

        Pokemon pokemon = new Pokemon();
        pokemon.PokeId = pokemonJson.id;
        pokemon.correctedName = correctPokemonName(pokemonJson.name);
        pokemon.name = pokemon.correctedName.ToProper();
        pokemon.BaseExperience = pokemonJson.base_experience;
        pokemon.height = pokemonJson.height;
        if (PokemonConstants.enumToTier.ContainsKey(pokemon.PokeId)) pokemon.tier = PokemonConstants.enumToTier[pokemon.PokeId];

        // Stats
        foreach (var pokeStat in pokemonJson.stats)
        {
            switch (pokeStat.stat.name)
            {
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

        // Types
        pokemon.types[0] = PokemonUtil.StringToType(pokemonJson.types.Find((type) => type.slot == 1).type.name);
        if (pokemonJson.types.Count > 1) pokemon.types[1] = PokemonUtil.StringToType(pokemonJson.types.Find((type) => type.slot == 2).type.name);

        // Abilities
        Ability ability;
        if (hasHiddenAbility)
        {
            ability = pokemonJson.abilities.Find((ability_) => ability_.is_hidden == true);
        }
        else
        {
            // Remove the hidden ability from the list
            pokemonJson.abilities = pokemonJson.abilities.FindAll((ability_) => ability_.is_hidden == false);
            ability = pokemonJson.abilities[new System.Random().Next(pokemonJson.abilities.Count)];
        }
        pokemon.Ability = new PokemonAbility()
        {
            name = ability.ability.name.ToProper().Replace("-", " "),
            isHidden = ability.is_hidden,
            slot = ability.slot,
            url = ability.ability.url
        };

        logger.LogInformation("Fetching pokemon ability: " + pokemon.name, LogLevel.All);
        var abilityJson = await http.GetAsync<AbilityWithDescription>(ability.ability.url);
        AbilityEffect effect = abilityJson.effect_entries.Find((effect_) => effect_.language.name == "en");
        pokemon.Ability.description = effect.short_effect;
        pokemon.Ability.longDescription = effect.effect;

        // Species
        logger.LogInformation("Fetching pokemon species: " + pokemon.name, LogLevel.All);
        var species = await http.GetAsync<PokemonSpecies>(pokemonJson.species.url);

        // Evolution Chain
        logger.LogInformation("Fetching pokemon evolution chain: " + pokemon.name, LogLevel.All);
        var evolutionChain = await http.GetAsync<EvolutionChain>(species.evolution_chain.url);
        pokemon.Evolutions = evolutionChain.GetEvolutions();
        pokemon.EvolutionStage = evolutionChain.GetEvolutionStage(pokemonJson.name);

        // Textures
        logger.LogInformation("Fetching pokemon sprites: " + pokemon.name, LogLevel.All);
        byte[] texture = await http.GetTexture2DAsync(pokemonJson.sprites.versions["generation-v"]["black-white"].front_default);
        //pokemon.Sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 1f);
        //pokemon.ShopSprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 1f);
        //pokemon.TrueSpriteSize = TextureUtil.GetTrueSizeInPixels(pokemon.Sprite.texture, 0f);
        return pokemon;
    }
}
