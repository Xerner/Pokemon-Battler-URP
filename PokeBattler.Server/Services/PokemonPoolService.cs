using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using PokeBattler.Common.Models;
using PokeBattler.Server.Extensions;
using PokeBattler.Server.Services.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokeBattler.Server.Services;

public interface IPokemonPoolService
{
    public int[] GetTierChances(int trainerLevel);
    public void InitializeFromCache(PokemonPool pokemonPool);
    public Pokemon[] Withdraw5(PokemonPool pokemonPool, int trainerLevel);
    public void Refund(PokemonPool pokemonPool, IEnumerable<Pokemon> pokemons);
}

public class PokemonPoolService(ILogger<PokemonPoolService> logger, PokeAPIService pokemonService)
{
    public int[] GetTierChances(int trainerLevel)
    {
        return Constants.TierChances[trainerLevel];
    }

    public void InitializeFromCache(PokemonPool pokemonPool)
    {
        logger.LogInformation($"Initializing Pokemon PokemonPoolService with {pokemonService.CachedPokemon.Count} different Pokemon", LogLevel.Detailed);
        foreach (string pokemonName in pokemonService.CachedPokemon.Keys)
        {
            Pokemon pokemon = pokemonService.CachedPokemon[pokemonName];
            if (pokemon.EvolutionStage == 1) pokemonPool.TierToPokemonCounts[pokemon.tier].Add(pokemon.name, Constants.TierCounts[pokemon.tier]);
        }
        logger.LogInformation($"Initialized PokemonPoolService with {pokemonService.CachedPokemon.Count} different Pokemon", LogLevel.Detailed);
    }

    /// <summary>Withdraws 5 Pokemon from the PokemonPoolService randomly with respect to the trainers level</summary>
    // TODO: RequestAddToGame method for when other players withdraw pokemon. Figuring out how to make the data secure will be tricky
    public Pokemon[] Withdraw5(PokemonPool pokemonPool, int trainerLevel)
    {
        //Debug2.Log($"Trainer {trainersService.Account.Settings.Username} is withdrawing pokemon");
        Pokemon[] pokemons = new Pokemon[5];
        for (int i = 0; i < pokemons.Length; i++)
        {
            pokemons[i] = withdraw(pokemonPool, trainerLevel);
        }
        logger.LogInformation($"Pokemon withdrawed: " + string.Join(',', pokemons.Select(pokemon => pokemon.name)), LogLevel.Detailed);
        return pokemons;
    }

    /// <summary>Attempt to withdraw 1 Pokemon from the PokemonPoolService</summary>
    /// <returns>A random Pokemon from the given tier, or null if there are no Pokemon left to pull</returns>
    private Pokemon withdraw(PokemonPool pokemonPool, int trainerLevel)
    {
        // roll for which pokemon to withdraw
        int tier = rollForTier(trainerLevel);
        var possiblePokemon = getPossiblePokemonToWithdrawFromTier(tier, 1);
        int roll = getRollForAPokemon(pokemonPool, tier, possiblePokemon);
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
                roll = getRollForAPokemon(pokemonPool, tier, possiblePokemon);
                randomPokemon = possiblePokemon[roll];
                // repeat if 0 pulls left on the newly randomized Pokemon
            } while (pokemonPool.TierToPokemonCounts[tier][randomPokemon.name] <= 0);
        }
        else
        {
            logger.LogInformation($"There are no tier {tier} pokemon left in the game!", LogLevel.Detailed);
            return null;
        }
        pokemonPool.TierToPokemonCounts[tier][randomPokemon.name]--;
        return randomPokemon;
    }

    private List<Pokemon> getPossiblePokemonToWithdrawFromTier(int tier, int highestEvolutionStagePossible)
    {
        return pokemonService.TierToPokemonList[tier].Where(pokemon => pokemon.EvolutionStage == highestEvolutionStagePossible).ToList();
    }

    public void Refund(PokemonPool pokemonPool, Pokemon[] pokemons)
    {
        foreach (var pokemon in pokemons) pokemonPool.TierToPokemonCounts[pokemon.tier][pokemon.name]++;
        //debugPanel.UpdatePokemonDebugContent(TrainerToShopPokemon, this);
    }

    /// <summary>Internal helper method</summary>
    private int getRollForAPokemon(PokemonPool pokemonPool, int tier, List<Pokemon> pokemonCounts, bool remove = false)
    {
        var random = new Random();
        int roll = random.Next(0, pokemonCounts.Count + 1);
        Pokemon randomPokemon = pokemonCounts[roll];
        // we have rolled this Pokemon once, we do not want to roll for it again
        if (remove) pokemonCounts.RemoveAt(roll);
        //
        if (pokemonPool.TierToPokemonCounts[tier][randomPokemon.name] <= 0)
            logger.LogInformation($"Cannot withdraw a {randomPokemon.name}! 0 left", LogLevel.All);
        return roll;
    }

    /// <summary>Roll for what tier of Pokemon the trainer will pull from the PokemonPoolService</summary>
    /// <returns>The tier of unit to pull from the Pokemon PokemonPoolService</returns>
    private int rollForTier(int trainerLevel)
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
            chanceSum += Constants.TierChances[trainerLevel, i];
            if (roll < chanceSum)
            {
                return i + 1;
            }
        }
        throw new Exception("Failed all tier chances when rolling for a Pokemon from the Pokemon PokemonPoolService!");
    }

    /// <summary>
    /// Recursively evolve the pokemon until it can't no more
    /// </summary>
    public void Evolve(TrainersPokemon trainersPokemon, Pokemon pokemon)
    {
        var pokemonToDestroy = new List<Guid>();
        bool evolving = trainersPokemon.IsAboutToEvolve(pokemon);
        if (!evolving)
        {
            return;
        }
        // Destroy all other instances of this Pokemon
        foreach (Pokemon otherPokemon in trainersPokemon.ActivePokemon[pokemon.name])
        {
            if (pokemon != otherPokemon)
            {
                pokemonToDestroy.Add(otherPokemon.Id);
            }
        }
        trainersPokemon.ActivePokemon.Remove(pokemon.name);
        // Evooooolve
        pokemon.Evolve();
        // recursion is intended here
        // If the evolved Pokemon is about to evolve, then it will be evolved again
        Evolve(trainersPokemon, pokemon);
        return pokemonToDestroy;
    }

    /// <summary>PokemonPoolService Constants</summary>
    public class Constants
    {
        /// <summary>How many Pokemon are allowed in each respective tier</summary>
        public static readonly int[] TierCounts = [0, 39, 26, 21, 13, 10]; // 0 for tier 0, because technically there is no tier 0
        /// <summary>Chance to roll a 1 tier, 2 tier, 3 tier, 4 tier, 5 tier respective to Trainer level</summary>
        public static readonly List<int[]> TierChances = new () {
            new [] {100, 0, 0, 0, 0},
            new [] {100, 0, 0, 0, 0},
            new [] {75, 25, 0, 0, 0},
            new [] {55, 30, 15, 0, 0},
            new [] {45, 33, 20, 2, 0},
            new [] {25, 40, 30, 5, 0},
            new [] {19, 30, 35, 15, 1},
            new [] {16, 20, 35, 25, 4},
            new [] {9, 15, 30, 30, 16}
        };
    }
}
