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

    /// <summary>
    /// Returns a valid Pokemon name for querying Poke API from the given Pokemon name
    /// </summary>
    /// <param name="name">A possibly valid Pokemon name</param>
    /// <returns>A valid Pokemon name, or an empty string if the given name was invalid</returns>
    public static string GetValidPokemonName(string name) {
        foreach (Enum pokeName in Enum.GetValues(typeof(PokemonConstants.PokemonName))) {
            if (name.Trim().ToLower() == pokeName.ToString().ToLower()) return pokeName.ToString();
        }
        return "";
    }

    public class PokemonStat {
        public int baseStat;
        public int effort;
    }
}
