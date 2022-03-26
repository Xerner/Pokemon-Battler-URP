using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trainer
{
    public Account Account;
    public Arena Arena;
    [SerializeField] private TrainerCard TrainerCard;
    private bool Ready;
    private int CurrentHealth;
    private int TotalHealth;
    private int Level;
    private int Experience;
    public int Money;
    private Dictionary<string, List<PokemonBehaviour>> activePokemon;

    public Dictionary<string, List<PokemonBehaviour>> ActivePokemon { get; private set; }

    public bool AddPokemon(PokemonBehaviour pokemon)
    {
        PokeContainer container = Arena.Party.NextOpen();
        bool evolving = IsAboutToEvolve(pokemon.name);
        if (container == null && !evolving) return false; // no fucking room
        if (evolving)
        {
            pokemon = pokemon.Evolve();
            DeletePokemonList(pokemon.name);
        }
        else
        {
            container.SetPokemon(pokemon);
        }
        if (!ActivePokemon.ContainsKey(pokemon.name))
        {
            ActivePokemon.Add(pokemon.name, new List<PokemonBehaviour>());
        }
        ActivePokemon[pokemon.name].Add(pokemon);
        pokemon.OnDestroyed += (PokemonBehaviour pokemon) => ActivePokemon[pokemon.name].Remove(pokemon);
        return true;
    }

    public bool IsAboutToEvolve(string pokemonName)
    {
        return activePokemon.ContainsKey(pokemonName)
            && ActivePokemon[pokemonName].Count > 1
            && activePokemon[pokemonName][0].Evolution != null;
    }

    private void DeletePokemonList(string pokemonName)
    {
        foreach (PokemonBehaviour pokemon in ActivePokemon[pokemonName])
        {
            Object.Destroy(pokemon);
        }
        ActivePokemon.Remove(pokemonName);
    }
}
