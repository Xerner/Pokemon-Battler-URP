using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonBehaviour : MonoBehaviour
{
    public int tier;
    [HideInInspector] public string id;
    [HideInInspector] public PokeContainer CurrentField;
    [SerializeField] private PokemonBehaviour evolution;
    [SerializeField] private PokemonBehaviour baseEvolution;
    [HideInInspector] public Trainer trainer;
    public PokemonType type1;
    public PokemonType type2;
    public int EvolutionStage;
    //public bool isSelected;
    public bool combatMode;
    public ArenaCard combatField;
    public Allegiance Allegiance;
    private PokemonBehaviour targetEnemy;
    //private Pathing Path;
    public bool invulnerable;

    public Action<PokemonBehaviour> OnDestroyed;

    public PokemonBehaviour Evolution { get; private set; }

    private void OnDestroy()
    {
        if (CurrentField == null) throw new System.Exception($"{id}\t{name} : CurrentField == null!");
        CurrentField.Reset();
        OnDestroyed?.Invoke(this);
    }

    public PokemonBehaviour Evolve()
    {
        PokemonBehaviour evolution = Instantiate(this.evolution, transform).GetComponent<PokemonBehaviour>();
        evolution.id = id;
        //Network.IDtoPokemon[id] = evolution;
        Destroy(this);
        CurrentField.SetPokemon(evolution);
        return evolution;
    }
}
