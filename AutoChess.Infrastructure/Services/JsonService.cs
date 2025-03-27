using System.Text.Json;
using Microsoft.Extensions.Logging;
using AutoChess.Contracts.Models.Json;
using AutoChess.Contracts.Models;
using AutoChess.Infrastructure.Interfaces;
using AutoChess.Library.Extensions;
using AutoChess.Library.Services;
using AutoChess.Library.Models;

namespace AutoChess.Infrastructure.Services;

public class JsonService(ILogger<JsonService> logger, IHttpService http) : IJsonService
{
    /// <summary>
    /// Builds a Pokemon from json
    /// </summary>
    /// <param name="json">json returned from the Poke API</param>
    public async Task<Pokemon> PokemonFromJson(string json, bool hasHiddenAbility)
    {
        // TODO: split up the different HTTP calls in this method into their own JSON deserialization classes
        var pokemonJson = JsonSerializer.Deserialize<PokemonJson>(json);

        Pokemon pokemon = new Pokemon();
        pokemon.PokeId = pokemonJson.id;
        pokemon.CorrectedName = CorrectPokemonName(pokemonJson.name);
        pokemon.Name = pokemon.CorrectedName.ToProper();
        pokemon.BaseExperience = pokemonJson.base_experience;
        pokemon.Height = pokemonJson.height;
        if (PokemonConstants.enumToTier.ContainsKey(pokemon.PokeId))
        {
            pokemon.Tier = PokemonConstants.enumToTier[pokemon.PokeId];
        }

        // Stats
        foreach (var pokeStat in pokemonJson.stats)
        {
            switch (pokeStat.stat.name)
            {
                case "hp":
                    pokemon.Hp.BaseStat = pokeStat.base_stat;
                    pokemon.Hp.Effort = pokeStat.effort;
                    break;
                case "attack":
                    pokemon.Attack.BaseStat = pokeStat.base_stat;
                    pokemon.Attack.Effort = pokeStat.effort;
                    break;
                case "defense":
                    pokemon.Defense.BaseStat = pokeStat.base_stat;
                    pokemon.Defense.Effort = pokeStat.effort;
                    break;
                case "special-attack":
                    pokemon.SpecialAttack.BaseStat = pokeStat.base_stat;
                    pokemon.SpecialAttack.Effort = pokeStat.effort;
                    break;
                case "special-defense":
                    pokemon.SpecialDefense.BaseStat = pokeStat.base_stat;
                    pokemon.SpecialDefense.Effort = pokeStat.effort;
                    break;
                case "speed":
                    pokemon.Speed.BaseStat = pokeStat.base_stat;
                    pokemon.Speed.Effort = pokeStat.effort;
                    break;
                default:
                    throw new Exception("Invalid Pokemon stat name: " + pokeStat.stat.name);
            }
        }

        // Types
        pokemon.Types[0] = PokemonUtil.StringToType(pokemonJson.types.Find((type) => type.slot == 1).type.name);
        if (pokemonJson.types.Count > 1)
        {
            pokemon.Types[1] = PokemonUtil.StringToType(pokemonJson.types.Find((type) => type.slot == 2).type.name);
        }

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
            ability = pokemonJson.abilities[new Random().Next(pokemonJson.abilities.Count)];
        }
        pokemon.Ability = new PokemonAbility()
        {
            name = ability.ability.name.ToProper().Replace("-", " "),
            isHidden = ability.is_hidden,
            slot = ability.slot,
            url = ability.ability.url
        };

        logger.LogInformation("Fetching pokemon ability: " + pokemon.Name);
        var abilityJson = await http.GetAsync<AbilityWithDescription>(ability.ability.url);
        AbilityEffect effect = abilityJson.effect_entries.Find((effect_) => effect_.language.name == "en");
        pokemon.Ability.description = effect.short_effect;
        pokemon.Ability.longDescription = effect.effect;

        // Species
        logger.LogInformation("Fetching pokemon species: " + pokemon.Name);
        var species = await http.GetAsync<PokemonSpecies>(pokemonJson.species.url);

        // Evolution Chain
        logger.LogInformation("Fetching pokemon evolution chain: " + pokemon.Name);
        var evolutionChain = await http.GetAsync<EvolutionChain>(species.evolution_chain.url);
        pokemon.Evolutions = evolutionChain.GetEvolutions();
        pokemon.EvolutionStage = evolutionChain.GetEvolutionStage(pokemonJson.name);

        // Textures
        logger.LogInformation("Fetching pokemon sprites: " + pokemon.Name);
        byte[] texture = await http.GetTexture2DAsync(pokemonJson.sprites.versions["generation-v"]["black-white"].front_default);
        //pokemon.Sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 1f);
        //pokemon.ShopSprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 1f);
        //pokemon.TrueSpriteSize = TextureUtil.GetTrueSizeInPixels(pokemon.Sprite.texture, 0f);
        return pokemon;
    }

    private string CorrectPokemonName(string pokemonName)
    {
        switch (pokemonName)
        {
            case "nidoran-m":
                return "nidoran";
            default:
                return pokemonName.ToLower();
        }
    }
}
