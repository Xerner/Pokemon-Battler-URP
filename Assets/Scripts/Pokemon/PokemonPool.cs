using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonPool {
    /// <summary>The count of each Pokemon left in a game</summary>
    public Dictionary<int, Dictionary<string, int>> TierToPokemonCounts = new Dictionary<int, Dictionary<string, int>>() {
        { 1, new Dictionary<string, int>() },
        { 2, new Dictionary<string, int>() },
        { 3, new Dictionary<string, int>() },
        { 4, new Dictionary<string, int>() },
        { 5, new Dictionary<string, int>() }
    };
    

    public PokemonPool() {
        Debug2.Log($"Initializing Pokemon pool counts with {Pokemon.CachedPokemon.Count} different Pokemon", LogLevel.Detailed);
        foreach (string pokemonName in Pokemon.CachedPokemon.Keys) {
            Pokemon pokemon = Pokemon.CachedPokemon[pokemonName];
            TierToPokemonCounts[pokemon.tier].Add(pokemon.name, Constants.TierCounts[pokemon.tier]);
        }
        Debug2.Log($"Initialized pool with {Pokemon.CachedPokemon.Count} different Pokemon", LogLevel.Detailed);
    }

    /// <summary>
    /// Withdraws 5 Pokemon from the pool randomly with respect to the trainers level
    /// </summary>
    /// <param name="trainer"></param>
    /// <returns></returns>
    public Pokemon[] Withdraw5(Trainer trainer) {
        Debug2.Log($"Trainer {trainer.Account.settings.Username} is withdrawing pokemon");
        Pokemon[] pokemons = new Pokemon[5];
        for (int i = 0; i < pokemons.Length; i++) {
            int tierRoll = rollForTier(trainer.Level);
            pokemons[i] = withdraw(tierRoll);
        }
        Debug2.Log($"Pokemon withdrawed: "+pokemons.ToString(), LogLevel.Detailed);
        return pokemons;
    }

    /// <summary>
    /// Attempt to withdraw 1 Pokemon from the pool
    /// </summary>
    /// <returns>A random Pokemon from the given tier, or null if there are no Pokemon left to pull</returns>
    private Pokemon withdraw(int tier) {
        // roll for which pokemon to withdraw
        int roll = getRollForAPokemon(tier, Pokemon.TierToPokemonList[tier]);
        Pokemon randomPokemon = Pokemon.TierToPokemonList[tier][roll];
        // if there are pulls left, then we are good to go
        if (TierToPokemonCounts[tier][randomPokemon.name] > 0) {
            TierToPokemonCounts[tier][randomPokemon.name]--;
            return randomPokemon;
        }
        // in the case that the randomly pulled pokemon has 0 pulls left, we will need to query again for a different pokemon
        // this list is created to satisfy this case
        var possiblePokemon = new List<Pokemon>(Pokemon.TierToPokemonList[tier]);
        if (possiblePokemon.Count > 0) {
            do {
                possiblePokemon.RemoveAt(roll);
                roll = getRollForAPokemon(tier, possiblePokemon);
                randomPokemon = possiblePokemon[roll];
                // repeat if 0 pulls left on the newly randomized Pokemon
            } while (TierToPokemonCounts[tier][randomPokemon.name] <= 0);
        } else {
            Debug2.LogError($"There are no tier {tier} pokemon left in the game!", LogLevel.Detailed);
            return null;
        }
        TierToPokemonCounts[tier][randomPokemon.name]--;
        return randomPokemon;
    }

    public void Refund(Pokemon[] pokemons) {
        foreach (var pokemon in pokemons) {
            TierToPokemonCounts[pokemon.tier][pokemon.name]++;
        }
    }

    /// <summary>Internal helper method</summary>
    private int getRollForAPokemon(int tier, List<Pokemon> pokemonCounts, bool remove = false) {
        int roll = Random.Range(0, pokemonCounts.Count);
        Pokemon randomPokemon = pokemonCounts[roll];
        if (remove) pokemonCounts.RemoveAt(roll);
        if (TierToPokemonCounts[tier][randomPokemon.name] <= 0)
            Debug2.Log($"Cannot withdraw a {randomPokemon.name}! 0 left", LogLevel.All);
        return roll;
    }

    private int rollForTier(int trainerLevel) {
        float roll = Random.Range(1f, 100f);
        if (roll < Constants.TierChances[trainerLevel, 0]) {
            return 0;
        } else if (roll < Constants.TierChances[trainerLevel, 1]) {
            return 1;
        } else if (roll < Constants.TierChances[trainerLevel, 2]) {
            return 2;
        } else if (roll < Constants.TierChances[trainerLevel, 3]) {
            return 3;
        } else if (roll < Constants.TierChances[trainerLevel, 4]) {
            return 4;
        }
        return 0;
    }

    /// <summary>PokemonPool Constants</summary>
    public class Constants {
        /// <summary>How many Pokemon are allowed in each respective tier</summary>
        public static readonly int[] TierCounts = new int[5] { 39, 26, 21, 13, 10 };
        /// <summary>Chance to roll a 1 tier, 2 tier, 3 tier, 4 tier, 5 tier respective to Trainer level</summary>
        public static readonly int[,] TierChances = new int[9, 5] {
            {100, 0, 0, 0, 0},
            {100, 0, 0, 0, 0},
            {75, 100, 0, 0, 0},
            {55, 85, 100, 0, 0},
            {45, 78, 98, 100, 0},
            {35, 70, 95, 100, 0},
            {22, 57, 87, 99, 100},
            {15, 40, 75, 95, 100},
            {10, 25, 55, 85, 100}
        };
    }
}
