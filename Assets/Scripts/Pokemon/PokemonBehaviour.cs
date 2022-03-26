using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Pokemon;

public class PokemonBehaviour : MonoBehaviour
{
    public Pokemon pokemon;
    [HideInInspector] public string id;
    [HideInInspector] public PokeContainer CurrentField;
    [HideInInspector] public Trainer trainer;
    //public bool isSelected;
    public bool combatMode;
    public ArenaCard combatField;
    public Allegiance Allegiance;
    private PokemonBehaviour targetEnemy;
    //private Pathing Path;
    public bool invulnerable;

    public Action<PokemonBehaviour> OnDestroyed;

    private void OnDestroy()
    {
        if (CurrentField == null) throw new System.Exception($"{id}\t{name} : CurrentField == null!");
        CurrentField.Reset();
        OnDestroyed?.Invoke(this);
    }

    public PokemonBehaviour Evolve()
    {
        PokemonBehaviour evolution = Instantiate(pokemon.evolution, transform).GetComponent<PokemonBehaviour>();
        evolution.id = id;
        //Network.IDtoPokemon[id] = evolution;
        Destroy(this);
        CurrentField.SetPokemon(evolution);
        return evolution;
    }
}
