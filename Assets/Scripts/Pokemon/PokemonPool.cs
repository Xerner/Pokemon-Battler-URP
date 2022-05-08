using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokemonPool {
    private static PokemonPool instance = null;
    private static readonly object padlock = new object();
    private static DebugContent staticDebugContent;
    private static Dictionary<string, UnityEngine.GameObject> debugContentPokemonGO = new Dictionary<string, UnityEngine.GameObject>();

    public static PokemonPool Instance {
        get {
            lock (padlock) {
                if (instance == null) instance = new PokemonPool();
                return instance;
            }
        }
    }

    /// <summary>The count of each Pokemon left in a game</summary>
    public Dictionary<int, Dictionary<string, int>> TierToPokemonCounts = new Dictionary<int, Dictionary<string, int>>() {
        { 1, new Dictionary<string, int>() },
        { 2, new Dictionary<string, int>() },
        { 3, new Dictionary<string, int>() },
        { 4, new Dictionary<string, int>() },
        { 5, new Dictionary<string, int>() }
    };

    public PokemonPool() {
        Debug2.Log($"Initializing Pokemon pool with {Pokemon.CachedPokemon.Count} different Pokemon", LogLevel.Detailed);
        foreach (string pokemonName in Pokemon.CachedPokemon.Keys) {
            Pokemon pokemon = Pokemon.CachedPokemon[pokemonName];
            if (pokemon.EvolutionStage == 1) TierToPokemonCounts[pokemon.tier].Add(pokemon.name, Constants.TierCounts[pokemon.tier]);
        }
        staticDebugContent = DebugPanelManager.Spawn("Pokemon Pool");
        InitializeDebugContent();
        Debug2.Log($"Initialized pool with {Pokemon.CachedPokemon.Count} different Pokemon", LogLevel.Detailed);
    }

    /// <summary>Withdraws 5 Pokemon from the pool randomly with respect to the trainers level</summary>
    // TODO: Create method for when other players withdraw pokemon. Figuring out how to make the data secure will be tricky
    public Pokemon[] Withdraw5() {
        Debug2.Log($"Trainer {TrainerManager.ActiveTrainer.Account.settings.Username} is withdrawing pokemon");
        Pokemon[] pokemons = new Pokemon[5];
        for (int i = 0; i < pokemons.Length; i++) {
            pokemons[i] = withdraw();
        }
        UpdatePokemonDebugContent(pokemons);
        Debug2.Log($"Pokemon withdrawed: " + string.Join(',', pokemons.Select(pokemon => pokemon.name)), LogLevel.Detailed);
        return pokemons;
    }

    /// <summary>Attempt to withdraw 1 Pokemon from the pool</summary>
    /// <returns>A random Pokemon from the given tier, or null if there are no Pokemon left to pull</returns>
    private Pokemon withdraw() {
        // roll for which pokemon to withdraw
        int tier = rollForTier(TrainerManager.ActiveTrainer.Level);
        var possiblePokemon = getPossiblePokemonToWithdrawFromTier(tier, 1);
        int roll = getRollForAPokemon(tier, possiblePokemon);
        Pokemon randomPokemon = possiblePokemon[roll];
        // if there are pulls left, then we are good to go
        if (TierToPokemonCounts[tier][randomPokemon.name] > 0) {
            TierToPokemonCounts[tier][randomPokemon.name]--;
            return randomPokemon;
        }
        // in the case that the randomly pulled pokemon has 0 pulls left, we will need to query again for a different pokemon
        // this list is created to satisfy this case
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

    private List<Pokemon> getPossiblePokemonToWithdrawFromTier(int tier, int highestEvolutionStagePossible) {
        return Pokemon.TierToPokemonList[tier].Where(pokemon => pokemon.EvolutionStage == highestEvolutionStagePossible).ToList();
    }

    public void Refund(Pokemon[] pokemons) {
        foreach (var pokemon in pokemons) TierToPokemonCounts[pokemon.tier][pokemon.name]++;
        UpdatePokemonDebugContent(pokemons);
    }

    /// <summary>Internal helper method</summary>
    private int getRollForAPokemon(int tier, List<Pokemon> pokemonCounts, bool remove = false) {
        int roll = Random.Range(0, pokemonCounts.Count);
        Pokemon randomPokemon = pokemonCounts[roll];
        // we have rolled this Pokemon once, we do not want to roll for it again
        if (remove) pokemonCounts.RemoveAt(roll);
        //
        if (TierToPokemonCounts[tier][randomPokemon.name] <= 0)
            Debug2.Log($"Cannot withdraw a {randomPokemon.name}! 0 left", LogLevel.All);
        return roll;
    }

    /// <summary>Roll for what tier of Pokemon the trainer will pull from the pool</summary>
    /// <returns>The tier of unit to pull from the Pokemon pool</returns>
    private int rollForTier(int trainerLevel) {
        float roll = Random.Range(1f, 100f);
        int chanceSum = 0;
        // Example: 
        // rolls 90, trainer level 3
        // {75, 25, 0, 0, 0}
        // 90 < 75 for a tier 1? no
        // 25 + 75 = 95
        // 90 < 95 for a tier 2? yes
        // rolls a tier 2 unit
        for (int i = 0; i < 5; i++) {
            chanceSum += Constants.TierChances[trainerLevel, i];
            if (roll < chanceSum) {
                return i+1;
            }
        }
        throw new System.Exception("Failed all tier chances when rolling for a Pokemon from the Pokemon Pool!");
    }

    /// <summary>PokemonPool Constants</summary>
    public class Constants {
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

    public void InitializeDebugContent() {
        staticDebugContent.Content.AddComponent<HorizontalLayoutGroup>().childControlHeight = false;
        foreach (var key in TierToPokemonCounts.Keys) {
            var tierGO = CreateDebugContent($"Tier {key}", staticDebugContent.Content.transform, false);
            tierGO.AddComponent<VerticalLayoutGroup>();
            var tierTextGO = CreateDebugContent($"Tier {key}", tierGO.transform, false);
            var tmesh = tierTextGO.GetComponent<TextMeshProUGUI>();
            tmesh.text = $"<b>Tier { key}</b>";
            tmesh.fontSize = 18f;
            foreach (var pokemon in TierToPokemonCounts[key]) {
                var pokemonGO = CreateDebugContent(pokemon.Key, tierGO.transform);
                SetPokemonDebugContent(pokemonGO, pokemon.Key, pokemon.Value);
            }
        }
        DebugPanelManager.UpdateSize();
    }

    private UnityEngine.GameObject CreateDebugContent(string name, Transform parent, bool addToDict = true) {
        var debugContent = new UnityEngine.GameObject(name);
        debugContent.transform.SetParent(parent);
        if (addToDict) debugContentPokemonGO.Add(name, debugContent);
        debugContent.AddComponent<CanvasRenderer>();
        var contentSizeFitter = debugContent.AddComponent<ContentSizeFitter>();
        contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        var tmesh = debugContent.AddComponent<TextMeshProUGUI>();
        tmesh.richText = true;
        tmesh.fontSize = 16f;
        return debugContent;
    }

    private void UpdatePokemonDebugContent(Pokemon[] pokemons) {
        foreach (var pokemon in pokemons) UpdatePokemonDebugContent(pokemon);
    }

    public void UpdatePokemonDebugContent(Pokemon pokemon) {
        SetPokemonDebugContent(debugContentPokemonGO[pokemon.name], pokemon.name, TierToPokemonCounts[pokemon.tier][pokemon.name]);
    }

    private void SetPokemonDebugContent(UnityEngine.GameObject gameObject, string name, int count) {
        gameObject.GetComponent<TextMeshProUGUI>().text = $"<b><color=#8888FF>{count}</b> <color=#FFFFFF>{name}";
    }
}
