using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pokemon", menuName = "Pokemon/Pokemon")]
public class Pokemon : ScriptableObject {
    private string Ability; // TODO: create an ability class
    private int BaseExperience; // TODO: make use of it, or get rid of it
    private int height; // TODO: make use of it, or get rid of it

    public PokemonStat hp = new PokemonStat() { baseStat = 50, effort = 0 };
    public PokemonStat attack = new PokemonStat() { baseStat = 50, effort = 0 };
    public PokemonStat defense = new PokemonStat() { baseStat = 50, effort = 0 };
    public PokemonStat specialAttack = new PokemonStat() { baseStat = 50, effort = 0 };
    public PokemonStat specialDefense = new PokemonStat() { baseStat = 50, effort = 0 };
    public PokemonStat speed = new PokemonStat() { baseStat = 50, effort = 0 };

    public EPokemonType[] types = new EPokemonType[] { EPokemonType.Normal, EPokemonType.None };

    public Pokemon evolution;
    public Pokemon baseEvolution;
    public int EvolutionStage;
    public int tier;

    [Header("Shop")]
    public Sprite shopSprite;

    public enum EPokemonType {
        None,
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

    public class PokemonStat {
        public int baseStat;
        public int effort;
    }
}
