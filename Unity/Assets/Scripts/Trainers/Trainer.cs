using Poke.Core;
using Poke.Unity;
using System.Collections.Generic;
using UnityEngine;

public class Trainer {
    public Account Account;
    public ArenaBehaviour Arena;
    [SerializeField] TrainerCard TrainerCard;
    bool Ready = false;
    int CurrentHealth = 100;
    int TotalHealth = 100;
    int experience = 0;
    public int Money = 10;
    int level = 2;
    public int Level { get => level; }
    public int Experience { get => experience; }
    public string ExperienceStr { get => experience + "/" + LevelToExpNeededToLevelUp[level]; }
    Dictionary<string, List<PokemonBehaviour>> activePokemon = new Dictionary<string, List<PokemonBehaviour>>();

    public static readonly int baseIncome = 5;
    public static readonly int pvpWinIncome = 1;
    public static Dictionary<int, int> winStreakIncome = new Dictionary<int, int>() {
        { 1, 0 },
        { 2, 1 },
        { 3, 1 },
        { 4, 2 },
        { 5, 3 }
    };
    public static Dictionary<int, int> LevelToExpNeededToLevelUp = new Dictionary<int, int>() {
        { 1, 0 },
        { 2, 2 },
        { 3, 6 },
        { 4, 10 },
        { 5, 20 },
        { 6, 36 },
        { 7, 56 },
        { 8, 80 },
        { 9, 100 }
    };

    public Trainer(Account account) {
        Account = account;
    }

    public int CalculateInterest() => Mathf.FloorToInt(Money / 10);

    /// <summary>Add experience to the Trainer. Rolls over exp if they level up.</summary>
    /// <returns>True if the player levels up</returns>
    public bool AddExperience(int expToAdd) {
        int expNeeded = LevelToExpNeededToLevelUp[level];
        int newExp = experience + expToAdd;
        int difference = expNeeded - newExp;
        if (difference <= 0) { // leveled up
            // edge case: player is already max level
            if (!LevelToExpNeededToLevelUp.ContainsKey(level+1)) {
                experience = LevelToExpNeededToLevelUp[level];
                difference = 1;
            } else {
                level++;
                experience = Mathf.Abs(difference);
            }
        } else {
            experience = newExp;
        }
        return difference <= 0;
    }

    public bool AddPokemonToBench(PokemonBehaviour pokemon) {
        
        BenchBehaviour bench = Arena.Bench.GetAvailableBench();
        // Evolve?
        bool evolving = IsAboutToEvolve(pokemon.Pokemon);
        if (bench == null && !evolving) return false; // no fucking room
        // evolve and/or set the PokemonBehaviour inside the container
        if (evolving) {
            pokemon = pokemon.Evolve();
            AssimilatePokemon(pokemon.Pokemon.name);
        }
        pokemon.MoveTo.ShouldLerpToPosition = false;
        bench.SetPokemon(pokemon);
        pokemon.MoveTo.ShouldLerpToPosition = true;
        // Add it to the Trainers ActivePokemon dictionary
        if (!activePokemon.ContainsKey(pokemon.name)) activePokemon.Add(pokemon.name, new List<PokemonBehaviour>());
        activePokemon[pokemon.name].Add(pokemon);
        pokemon.OnDestroyed += (PokemonBehaviour pokemon) => activePokemon[pokemon.name].Remove(pokemon);
        return true;
    }

    public bool IsAboutToEvolve(Pokemon pokemon)
    {
        return activePokemon.ContainsKey(pokemon.name)
            && pokemon.EvolutionStage < pokemon.Evolutions.Count - 1
            && activePokemon[pokemon.name].Count > 1
            && activePokemon[pokemon.name][0].Pokemon.Evolutions[pokemon.EvolutionStage+1] != null;
    }

    /// <summary>Assimilate the other Pokemon involved in an evolution. AKA Delete them from existence</summary>
    void AssimilatePokemon(string pokemonName)
    {
        foreach (PokemonBehaviour pokemon in activePokemon[pokemonName])
            Object.Destroy(pokemon);
        activePokemon.Remove(pokemonName);
    }
}
