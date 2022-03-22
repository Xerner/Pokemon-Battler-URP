using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    public int tier;
    [HideInInspector] public string id;
    [HideInInspector] public PokeContainer CurrentField;
    [SerializeField] private Pokemon evolution;
    [SerializeField] private Pokemon baseEvolution;
    [HideInInspector] public Trainer trainer;
    public PokemonType type1;
    public PokemonType type2;
    public int EvolutionStage;
    //public bool isSelected;
    public bool combatMode;
    public ArenaCard combatField;
    public Allegiance Allegiance;
    private Pokemon targetEnemy;
    //private Pathing Path;
    public bool invulnerable;

    public Action<Pokemon> OnDestroyed;

    public Pokemon Evolution { get; private set; }

    private void OnDestroy()
    {
        if (CurrentField == null) throw new System.Exception($"{id}\t{name} : CurrentField == null!");
        CurrentField.Reset();
        OnDestroyed?.Invoke(this);
    }

    public Pokemon Evolve()
    {
        Pokemon evolution = Instantiate(this.evolution, transform).GetComponent<Pokemon>();
        evolution.id = id;
        //Network.IDtoPokemon[id] = evolution;
        Destroy(this);
        CurrentField.SetPokemon(evolution);
        return evolution;
    }
}
