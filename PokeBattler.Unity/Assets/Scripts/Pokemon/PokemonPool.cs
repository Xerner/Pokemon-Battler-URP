using PokeBattler.Unity;
using PokeBattler.Services;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PokeBattler.Core {
    public class PokemonPool
    {
        /// <summary>The count of each Pokemon left in a game</summary>
        public Dictionary<int, Dictionary<string, int>> TierToPokemonCounts = new Dictionary<int, Dictionary<string, int>>() {
            { 1, new Dictionary<string, int>() },
            { 2, new Dictionary<string, int>() },
            { 3, new Dictionary<string, int>() },
            { 4, new Dictionary<string, int>() },
            { 5, new Dictionary<string, int>() }
        };

        TrainersService trainersService;

        public PokemonPool(TrainersService trainersService)
        {
            this.trainersService = trainersService;
            Debug2.Log($"Initializing Pokemon PokemonPool with {Pokemon.CachedPokemon.Count} different Pokemon", LogLevel.Detailed);
            foreach (string pokemonName in Pokemon.CachedPokemon.Keys)
            {
                Pokemon pokemon = Pokemon.CachedPokemon[pokemonName];
                if (pokemon.EvolutionStage == 1) TierToPokemonCounts[pokemon.tier].Add(pokemon.name, Constants.TierCounts[pokemon.tier]);
            }
            Debug2.Log($"Initialized PokemonPool with {Pokemon.CachedPokemon.Count} different Pokemon", LogLevel.Detailed);
        }

        /// <summary>Withdraws 5 Pokemon from the PokemonPool randomly with respect to the trainers level</summary>
        // TODO: Create method for when other players withdraw pokemon. Figuring out how to make the data secure will be tricky
        public Pokemon[] Withdraw5()
        {
            //Debug2.Log($"Trainer {trainersService.Account.Settings.Username} is withdrawing pokemon");
            Pokemon[] pokemons = new Pokemon[5];
            for (int i = 0; i < pokemons.Length; i++)
            {
                pokemons[i] = withdraw();
            }
            Debug2.Log($"Pokemon withdrawed: " + string.Join(',', pokemons.Select(pokemon => pokemon.name)), LogLevel.Detailed);
            return pokemons;
        }

        /// <summary>Attempt to withdraw 1 Pokemon from the PokemonPool</summary>
        /// <returns>A random Pokemon from the given tier, or null if there are no Pokemon left to pull</returns>
        private Pokemon withdraw()
        {
            // roll for which pokemon to withdraw
            int tier = rollForTier(trainersService.ClientsTrainer.Level);
            var possiblePokemon = getPossiblePokemonToWithdrawFromTier(tier, 1);
            int roll = getRollForAPokemon(tier, possiblePokemon);
            Pokemon randomPokemon = possiblePokemon[roll];
            // if there are pulls left, then we are good to go
            if (TierToPokemonCounts[tier][randomPokemon.name] > 0)
            {
                TierToPokemonCounts[tier][randomPokemon.name]--;
                return randomPokemon;
            }
            // in the case that the randomly pulled pokemon has 0 pulls left, we will need to query again for a different pokemon
            // this list is created to satisfy this case
            if (possiblePokemon.Count > 0)
            {
                do
                {
                    possiblePokemon.RemoveAt(roll);
                    roll = getRollForAPokemon(tier, possiblePokemon);
                    randomPokemon = possiblePokemon[roll];
                    // repeat if 0 pulls left on the newly randomized Pokemon
                } while (TierToPokemonCounts[tier][randomPokemon.name] <= 0);
            }
            else
            {
                Debug2.LogError($"There are no tier {tier} pokemon left in the game!", LogLevel.Detailed);
                return null;
            }
            TierToPokemonCounts[tier][randomPokemon.name]--;
            return randomPokemon;
        }

        private List<Pokemon> getPossiblePokemonToWithdrawFromTier(int tier, int highestEvolutionStagePossible)
        {
            return Pokemon.TierToPokemonList[tier].Where(pokemon => pokemon.EvolutionStage == highestEvolutionStagePossible).ToList();
        }

        public void Refund(Pokemon[] pokemons)
        {
            foreach (var pokemon in pokemons) TierToPokemonCounts[pokemon.tier][pokemon.name]++;
            //debugPanel.UpdatePokemonDebugContent(pokemons, this);
        }

        /// <summary>Internal helper method</summary>
        private int getRollForAPokemon(int tier, List<Pokemon> pokemonCounts, bool remove = false)
        {
            int roll = Random.Range(0, pokemonCounts.Count);
            Pokemon randomPokemon = pokemonCounts[roll];
            // we have rolled this Pokemon once, we do not want to roll for it again
            if (remove) pokemonCounts.RemoveAt(roll);
            //
            if (TierToPokemonCounts[tier][randomPokemon.name] <= 0)
                Debug2.Log($"Cannot withdraw a {randomPokemon.name}! 0 left", LogLevel.All);
            return roll;
        }

        /// <summary>Roll for what tier of Pokemon the trainer will pull from the PokemonPool</summary>
        /// <returns>The tier of unit to pull from the Pokemon PokemonPool</returns>
        private int rollForTier(int trainerLevel)
        {
            float roll = Random.Range(1f, 100f);
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
            throw new System.Exception("Failed all tier chances when rolling for a Pokemon from the Pokemon PokemonPool!");
        }

        /// <summary>PokemonPool Constants</summary>
        public class Constants
        {
            /// <summary>How many Pokemon are allowed in each respective tier</summary>
            public static readonly int[] TierCounts = new int[6] { 0, 39, 26, 21, 13, 10 }; // 0 for tier 0, because technically there is no tier 0
            /// <summary>Chance to roll a 1 tier, 2 tier, 3 tier, 4 tier, 5 tier respective to Trainer level</summary>
            public static readonly int[,] TierChances = new int[9, 5] {
                {100, 0, 0, 0, 0},
                {100, 0, 0, 0, 0},
                {75, 25, 0, 0, 0},
                {55, 30, 15, 0, 0},
                {45, 33, 20, 2, 0},
                {25, 40, 30, 5, 0},
                {19, 30, 35, 15, 1},
                {16, 20, 35, 25, 4},
                {9, 15, 30, 30, 16}
            };
        }
    }
}
