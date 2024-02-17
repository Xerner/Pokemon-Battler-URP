using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PokeBattler.Common.Models;
using PokeBattler.Common.Models.DTOs;
using PokeBattler.Server.Extensions;
using PokeBattler.Server.Models;

namespace PokeBattler.Server.Services;

public interface IPokemonPoolService
{
    public int[] GetTierChances(int trainerLevel);
    public Pokemon[] Withdraw5(PokemonPool pokemonPool, int trainerLevel);
    public void Refund(PokemonPool pokemonPool, IEnumerable<Pokemon> pokemons);
    public Task<BuyPokemonDTO> Evolve(TrainersPokemon trainersPokemon, Pokemon pokemon, List<Guid> pokemonToDestroy = null);
    public void Initialize(PokemonPool pokemonPool);
}

public class PokemonPoolService : IPokemonPoolService
{
    private static readonly string[] stringArray = [ "bulbasaur", "squirtle", "charmander", "magnemite", "abra" ];
    readonly ILogger<PokemonPoolService> logger;
    readonly IPokeApiService pokeApiService;

    public PokemonPoolService(ILogger<PokemonPoolService> logger,
                                IPokeApiService pokeApiService)
    {
        this.logger = logger;
        this.pokeApiService = pokeApiService;
        InitializeFromNames(stringArray);
    }

    public Dictionary<int, List<Pokemon>> TierToPokemonPool { get; set; } = new() {
        { 1, new List<Pokemon>() },
        { 2, new List<Pokemon>() },
        { 3, new List<Pokemon>() },
        { 4, new List<Pokemon>() },
        { 5, new List<Pokemon>() }
    };

    public int[] GetTierChances(int trainerLevel)
    {
        return Constants.TierChancesByLevel[trainerLevel];
    }

    public void Initialize(PokemonPool pokemonPool)
    {
        var pokemons = pokeApiService.InitializeFromCache(pokemonPool);
        AddToPool(pokemons);
    }

    public async Task InitializeFromNames(IEnumerable<string> names)
    {
        var pokemons = await pokeApiService.InitializeListOfPokemon(names);
        AddToPool(pokemons);
    }

    /// <summary>Withdraws 5 Pokemon from the PokemonPoolService randomly with respect to the trainers level</summary>
    // TODO: RequestAddToGame method for when other players withdraw pokemon. Figuring out how to make the data secure will be tricky
    public Pokemon[] Withdraw5(PokemonPool pokemonPool, int trainerLevel)
    {
        //Debug2.Log($"Trainer {trainersService.Account.Settings.Username} is withdrawing pokemon");
        Pokemon[] pokemons = new Pokemon[5];
        for (int i = 0; i < pokemons.Length; i++)
        {
            pokemons[i] = Withdraw(pokemonPool, trainerLevel);
        }
        logger.LogInformation($"Pokemon withdrawed: " + string.Join(',', pokemons.Select(pokemon => pokemon.name)));
        return pokemons;
    }
    
    public void Refund(PokemonPool pokemonPool, IEnumerable<Pokemon> pokemons)
    {
        foreach (var pokemon in pokemons)
        {
            pokemonPool.TierToPokemonCounts[pokemon.tier][pokemon.name]++;
        }
        //debugPanel.UpdatePokemonDebugContent(TrainerToShopPokemon, this);
    }
    
    /// <summary>
    /// Recursively evolve the pokemon until it can't no more
    /// </summary>
    public async Task<BuyPokemonDTO> Evolve(TrainersPokemon trainersPokemon, Pokemon pokemon, List<Guid> pokemonToDestroy = null)
    {
        if (!trainersPokemon.IsAboutToEvolve(pokemon))
        {
            return new BuyPokemonDTO()
            {
                Pokemon = pokemon,
                PokemonToDestroy = null
            };
        }
        // TODO: move this function to proper place
        if (pokemonToDestroy == null)
        {
            pokemonToDestroy = new List<Guid>();
        }
        bool evolving = trainersPokemon.IsAboutToEvolve(pokemon);
        if (!evolving)
        {
            return new BuyPokemonDTO()
            {
                Pokemon = pokemon,
                PokemonToDestroy = pokemonToDestroy
            };
        }
        // Destroy all other instances of this Pokemon
        foreach (Pokemon otherPokemon in trainersPokemon[pokemon.name])
        {
            if (pokemon != otherPokemon)
            {
                pokemonToDestroy.Add(otherPokemon.Id);
            }
        }
        trainersPokemon.Remove(pokemon.name);
        // Evooooolve
        var id = pokemon.Id;
        pokemon = await pokeApiService.GetEvolution(pokemon);
        pokemon.Id = id;
        // recursion is intended here
        // If the evolved Pokemon is about to evolve, then it will be evolved again
        return await Evolve(trainersPokemon, pokemon, pokemonToDestroy);
    }

    /// <summary>Attempt to withdraw 1 Pokemon from the PokemonPoolService</summary>
    /// <returns>A random Pokemon from the given tier, or null if there are no Pokemon left to pull</returns>
    Pokemon Withdraw(PokemonPool pokemonPool, int trainerLevel)
    {
        // roll for which pokemon to withdraw
        int tier = RollForTier(trainerLevel);
        var possiblePokemon = GetPossiblePokemonToWithdrawFromTier(tier, 1);
        int roll = GetRollForAPokemon(pokemonPool, tier, possiblePokemon);
        Pokemon randomPokemon = possiblePokemon[roll];
        // if there are pulls left, then we are good to go
        if (pokemonPool.TierToPokemonCounts[tier][randomPokemon.name] > 0)
        {
            pokemonPool.TierToPokemonCounts[tier][randomPokemon.name]--;
            return randomPokemon;
        }
        // in the case that the randomly pulled pokemon has 0 pulls left, we will need to query again for a different pokemon
        // this list is created to satisfy this case
        if (possiblePokemon.Count > 0)
        {
            do
            {
                possiblePokemon.RemoveAt(roll);
                roll = GetRollForAPokemon(pokemonPool, tier, possiblePokemon);
                randomPokemon = possiblePokemon[roll];
                // repeat if 0 pulls left on the newly randomized Pokemon
            } while (pokemonPool.TierToPokemonCounts[tier][randomPokemon.name] <= 0);
        }
        else
        {
            logger.LogInformation($"There are no tier {tier} pokemon left in the game!");
            return null;
        }
        pokemonPool.TierToPokemonCounts[tier][randomPokemon.name]--;
        return randomPokemon;
    }

    /// <summary>Internal helper method</summary>
    int GetRollForAPokemon(PokemonPool pokemonPool, int tier, List<Pokemon> pokemonCounts, bool remove = false)
    {
        var random = new Random();
        int roll = random.Next(0, pokemonCounts.Count + 1);
        Pokemon randomPokemon = pokemonCounts[roll];
        // we have rolled this Pokemon once, we do not want to roll for it again
        if (remove) pokemonCounts.RemoveAt(roll);
        //
        if (pokemonPool.TierToPokemonCounts[tier][randomPokemon.name] <= 0)
            logger.LogInformation($"Cannot withdraw a {randomPokemon.name}! 0 left");
        return roll;
    }

    void AddToPool(IEnumerable<Pokemon> pokemons)
    {
        foreach (var pokemon in pokemons)
        {
            AddToPool(pokemon);
        }
        logger.LogInformation($"Initialized Pokemon PokemonPoolService with {pokemons.Count()} different Pokemon");
    }

    void AddToPool(Pokemon pokemon)
    {
        TierToPokemonPool[pokemon.tier].Add(pokemon);
    }

    List<Pokemon> GetPossiblePokemonToWithdrawFromTier(int tier, int highestEvolutionStagePossible)
    {
        return TierToPokemonPool[tier].Where(pokemon => pokemon.EvolutionStage == highestEvolutionStagePossible).ToList();
    }

    /// <summary>Roll for what tier of Pokemon the trainer will pull from the PokemonPoolService</summary>
    /// <returns>The tier of unit to pull from the Pokemon PokemonPoolService</returns>
    int RollForTier(int trainerLevel)
    {
        var random = new Random();
        float roll = random.Next(1, 101);
        int chanceSum = 0;
        // Example: 
        // rolls 90, trainer level 3
        // {75, 25, 0, 0, 0}
        // 90 < 75 for a tier 1? no
        // 25 + 75 = 95
        // 90 < 95 for a tier 2? yes
        // rolls a tier 2 unit
        for (int i = 0; i < 5; i++)
        {
            chanceSum += Constants.TierChancesByLevel[trainerLevel][i];
            if (roll < chanceSum)
            {
                return i + 1;
            }
        }
        throw new Exception("Failed all tier chances when rolling for a Pokemon from the Pokemon PokemonPoolService!");
    }

    /// <summary>PokemonPoolService Constants</summary>
    public static class Constants
    {
        /// <summary>How many Pokemon are allowed in each respective tier</summary>
        public static readonly int[] TierCounts = [0, 39, 26, 21, 13, 10]; // 0 for tier 0, because technically there is no tier 0
        /// <summary>Chance to roll a 1 tier, 2 tier, 3 tier, 4 tier, 5 tier respective to Trainer level</summary>
        public static readonly List<int[]> TierChancesByLevel = [
            [ 100, 0,  0,  0,  0  ],
            [ 100, 0,  0,  0,  0  ],
            [ 75,  25, 0,  0,  0  ],
            [ 55,  30, 15, 0,  0  ],
            [ 45,  33, 20, 2,  0  ],
            [ 25,  40, 30, 5,  0  ],
            [ 19,  30, 35, 15, 1  ],
            [ 16,  20, 35, 25, 4  ],
            [ 9,   15, 30, 30, 16 ],
        ];
    }
}
