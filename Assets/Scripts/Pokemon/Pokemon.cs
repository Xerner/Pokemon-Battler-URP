using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pokemon", menuName = "Pokemon/Pokemon")]
public class Pokemon : ScriptableObject
{
    [PokemonTypeIcon]
    public EPokemonType type1;
    [PokemonTypeIcon]
    public EPokemonType type2;

    public Pokemon evolution;
    public Pokemon baseEvolution;
    public int EvolutionStage;
    public int tier;

    [Header("Shop")]
    public Sprite shopSprite;

    public enum EPokemonType {
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
