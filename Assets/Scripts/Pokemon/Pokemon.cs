using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pokemon", menuName = "Pokemon/Pokemon")]
public class Pokemon : ScriptableObject
{
    [Header("General")]
    [PokemonTypeIcon]
    public PokemonType type1;
    [PokemonTypeIcon]
    public PokemonType type2;
    public PokemonBehaviour evolution;
    public PokemonBehaviour baseEvolution;
    public int EvolutionStage;
    public int tier;

    [Header("Shop")]
    public Sprite shopSprite;

    public enum PokemonType {
        Mystery,
        Bug,
        Dark,
        Dragon,
        Electric,
        Fairy,
        Fighting,
        Fire,
        Flying,
        Ghost,
        Grass,
        Ground,
        Ice,
        Normal,
        Poison,
        Psychic,
        Rock,
        Steel,
        Water
    }
}


