using System.Collections.Generic;
using UnityEngine;

public class Trainer {
    public Account Account;
    public Arena Arena;
    [SerializeField] private TrainerCard TrainerCard;
    private bool Ready = false;
    private int CurrentHealth = 100;
    private int TotalHealth = 100;
    private int experience = 0;
    public int Money = 10;
    private int level = 2;
    public int Level { get => level; }
    public int Experience { get => experience; }
    private Dictionary<string, List<PokemonBehaviour>> activePokemon;
    public Dictionary<string, List<PokemonBehaviour>> ActivePokemon { get; private set; }

    public static readonly int baseIncome = 5;
    public static readonly int pvpWinIncome = 1;
    public static Dictionary<int, int> winStreakIncome = new Dictionary<int, int>() {
        { 1, 0 },
        { 2, 1 },
        { 3, 1 },
        { 4, 2 },
        { 5, 3 }
    };
    public static Dictionary<int, int> ExpToNextLevel = new Dictionary<int, int>() {
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

    public bool AddPokemonToBench(PokemonBehaviour pokemon) {
        PokeContainer container = Arena.Bench.GetAvailableBench();
        // Evolve?
        bool evolving = IsAboutToEvolve(pokemon.Pokemon);
        if (container == null && !evolving) return false; // no fucking room
        // evolve and/or set the PokemonBehaviour inside the container
        if (evolving) {
            pokemon = pokemon.Evolve();
            AssimilatePokemon(pokemon.name);
        } else {
            container.SetPokemon(pokemon);
        }
        // Add it to the Trainers ActivePokemon dictionary
        if (!ActivePokemon.ContainsKey(pokemon.name)) ActivePokemon.Add(pokemon.name, new List<PokemonBehaviour>()); 
        ActivePokemon[pokemon.name].Add(pokemon);
        pokemon.OnDestroyed += (PokemonBehaviour pokemon) => ActivePokemon[pokemon.name].Remove(pokemon);
        return true;
    }

    public bool IsAboutToEvolve(Pokemon pokemon)
    {
        return activePokemon.ContainsKey(pokemon.name)
            && pokemon.EvolutionStage < pokemon.Evolutions.Count - 1
            && ActivePokemon[pokemon.name].Count > 1
            && activePokemon[pokemon.name][0].Pokemon.Evolutions[pokemon.EvolutionStage+1] != null;
    }

    /// <summary>Assimilate the other Pokemon involved in an evolution. AKA Delete them from existence</summary>
    private void AssimilatePokemon(string pokemonName)
    {
        foreach (PokemonBehaviour pokemon in ActivePokemon[pokemonName])
            Object.Destroy(pokemon);
        ActivePokemon.Remove(pokemonName);
    }
}
